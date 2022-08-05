using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ERMapping
{
  /// <summary>
  /// Generate a group of folders containing markdown files describing all of the entities found in
  /// the Elden Ring fan API. If the files are created in an
  /// <see href="https://obsidian.md/">Obsidian</see> vault, a visual graph of connected components
  /// will be generated.
  /// </summary>
  class ERMapping
  {
    static readonly string BASE_URL = "https://eldenring.fanapis.com/api/";
    static readonly string SAVE_ROOT = @"bin\output\";
    static readonly string EXTRA_DIR_PATH = SAVE_ROOT + @"extras\";
    static readonly string[] ENDPOINTS = ERData.GetEndpoints();

    /// <summary>
    /// Retrieve a collection of all names that will be saved as graph nodes.
    /// </summary>
    /// <returns>A <c>string[]</c> of names of all graph nodes.</returns>
    private static async Task<string?[]> GetNames()
    {
      List<string?> names = new List<string?>(ERData.GetExtraNodes());
      foreach (var end in ENDPOINTS)
      {
        JArray res = await ERUtil.GetHTTPResource(BASE_URL + end);
        names = names.Concat(res.Select(x => ERUtil.ScrubText(x.Value<string>("name")!))).ToList();
      }
      return names.ToArray();
    }

    /// <summary>
    /// Adds markdown links to text. Obsidian recognizes markdown links as words in <c>[[double
    /// brackets]]</c>.
    /// </summary>
    /// <param name="text">The text <c>string</c> to add links to.</param>
    /// <param name="links">A <c>string[]</c> of links to find in the text.</param>
    /// <returns>The input <c>string</c> with markdown links added.</returns>
    private static string InsertLinks(string text, string?[] links)
    {
      string?[] combined = links;
      foreach (string? term in links)
      {
        // Match on the term if it is not inside square braces and is the beginning of a word.
        string patt = @"(\b"+ term + @")(\b|s)(?![^[]*\])";
        Regex r = new Regex(patt, RegexOptions.IgnoreCase);
        text = r.Replace(text, @"[[$1]]$2");
      }
      return text;
    }

    /// <summary>
    /// Create all markdown files in the <c>SAVE_ROOT</c> location.
    /// </summary>
    /// <returns><c>true</c> if generation succeeds; <c>false</c> otherwise.</returns>
    public static async Task<bool> GenerateFiles()
    {
      string?[] names = await GetNames();

      try
      {
        // Create the extra nodes that are not found in the API
        Directory.CreateDirectory(EXTRA_DIR_PATH);
        foreach (string extra in ERData.GetExtraNodes())
        {
          string path = EXTRA_DIR_PATH + char.ToUpper(extra[0]) + extra.Substring(1) + ".md";
          File.Create(path);
        }

        foreach (string endpoint in ENDPOINTS)
        {
          string destFolder = SAVE_ROOT + endpoint + @"\";
          Directory.CreateDirectory(destFolder);
          JArray res = await ERUtil.GetHTTPResource(BASE_URL + endpoint);

          foreach (JObject entry in res)
          {
            var props = entry.Properties().Select(x => x.Name);
            string path = ERUtil.ScrubText(destFolder + entry["name"] + ".md");

            string filecontents = $"# {entry["name"]}\n";
            filecontents += (entry.Value<string>("image") == String.Empty? "" : $"![|520]({entry["image"]})\n\n");
            filecontents += (props.Contains("region") ? $"## Region\n{entry["region"]}\n\n" : "");
            filecontents += (props.Contains("location") ? $"## Location\n{entry["location"]}\n\n" : "");
            if (props.Contains("drops"))
            {
              filecontents += "## Drops\n";
              filecontents += String.Join("", entry["drops"]!.Select(x => $"{x}\n")) + "\n";
            }
            filecontents += (props.Contains("description") ? $"## Description\n{entry["description"]}\n\n" : "");
            filecontents += (props.Contains("effects") ? $"## Effects\n{entry["effects"]}\n\n" : "");
            filecontents += (props.Contains("affinity") ? $"## Affinity\n{entry["affinity"]}\n\n" : "");
            filecontents += (props.Contains("skill") ? $"## Skill\n{entry["skill"]}\n\n" : "");
            filecontents += (props.Contains("weight") ? $"## Weight\n{entry["weight"]}\n\n" : "");
            filecontents += (props.Contains("role") ? $"## Role\n{entry["role"]}\n\n" : "");

            filecontents = InsertLinks(filecontents, names);

            using (StreamWriter sw = File.CreateText(path))
            {
              sw.WriteLine(filecontents);
            }
          }
        }

        return true;
      }
      catch (Exception exn)
      {
        Console.WriteLine(exn);
      }
      return false;
    }
  }
}