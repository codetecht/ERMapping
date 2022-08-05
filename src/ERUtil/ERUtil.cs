using Newtonsoft.Json.Linq;

namespace ERMapping
{
  /// <summary>
  /// Utilities for generating the ER Map.
  /// </summary>
  class ERUtil
  {
    static readonly int RESULT_LIMIT = 100; // API limit
    static readonly HttpClient CLIENT = new HttpClient();

    /// <summary>
    /// Requests data from the supplied endpoint.
    /// </summary>
    /// <param name="url">Endpoint to request data from</param>
    /// <returns><c>JArray</c> of results of the request</returns>
    public static async Task<JArray> GetHTTPResource(string url)
    {
      JArray? jsonData = new JArray();
      int? numResults = null;
      int received = 0;
      int page = 0;

      try
      {
        do
        {
          string formedUrl = $"{url}?page={page++}&limit={RESULT_LIMIT}";
          HttpResponseMessage res = await CLIENT.GetAsync(formedUrl);
          res.EnsureSuccessStatusCode();

          JObject intResult = JObject.Parse(await res.Content.ReadAsStringAsync());
          JArray? newData = intResult.Value<JArray>("data");
          numResults ??= intResult.Value<int>("total");
          received += intResult.Value<int>("count");
          jsonData = new JArray(jsonData.Concat(newData ?? new JArray()));
        }
        while (received < numResults);
      }
      catch(HttpRequestException exn)
      {
        Console.WriteLine(exn.Message);
      }
      return jsonData;
    }

    /// <summary>
    /// Alter text of entities.
    /// </summary>
    /// <param name="text">Text to be scrubbed.</param>
    /// <returns>The scrubbed text <c>string</c>.</returns>
    public static string ScrubText(string text)
    {
      if (!(text is object))
      {
        return "";
      }
      // Replace double quotes with single so the text can be a filename.
      text = text.Replace("\"", "\'");

      // Remove colons so the text can be a filename. The "Ash Of War" is created as an "Extra"
      // grouping instead.
      text = text.Replace("Ash Of War: ", null);
      return text;
    }
  }
}
