namespace ERMapping
{
  /// <summary>
  /// Collection of raw data to use when generating the ER Map.
  /// </summary>
  class ERData
  {

    /// <summary>
    /// Elden Ring fan API endpoints.
    /// </summary>
    /// <returns>A <c>string[]</c> of endpoints only, not including the base URL.</returns>
    public static string[] GetEndpoints()
    {
      return new string[]
      {
        "ammos",
        "armors",
        "ashes",
        "bosses",
        "classes",
        "creatures",
        "incantations",
        "items",
        "locations",
        "npcs",
        "shields",
        "sorceries",
        "spirits",
        "talismans",
        "weapons"
      };
    }

    /// <summary>
    /// Extra nodes for the ER Map.
    /// </summary>
    /// <returns>
    /// A <c>string[]</c> of extra nodes to place on the graph. These create meaningful groups
    /// of nodes which do not appear in the flavor text and would not be present otherwise.
    /// </returns>
    public static string[] GetExtraNodes()
    {
      return new string[] {
        // Weapons
        "dagger",
        "longsword",
        "greatsword",
        "straight sword",
        "curved sword",
        "curved club",
        "club",
        "sword",
        "katana",
        "hammer",
        "bow",
        "talisman",
        "trousers",
        "gauntlets",
        "staff",
        "claw",
        "longspear",

        // Armor
        "armor",
        "shield",
        "greatshield",
        "helm",
        "greaves",
        "hood",
        "hat",
        "bracers",
        "legwraps",
        "robe",
        "tabard",
        "gloves",
        "shoes",
        "mask",
        "breeches",

        // Damage Types
        "strike",
        "slash",
        "pierce",
        "magic",
        "fire",
        "lightning",
        "holy",
        "deadly poison",

        // Status
        "sleep",
        "madness",
        "frenzy",
        "poison",
        "bleed",

        // Items
        "Ash of War",
        "crafting",
        "craftable",
        "boluses",
        "Prattling Pate",
        "golden rune",
        "somber smithing stone",
        "smithing stone",
        "medallion",
        "map",
        "glovewort",
        "crystal tear",
        "tear",
        "key",
        "scroll",

        // Actionables
        "incantation",
        "sorcery",
        "ashes",

        // NPCs
        "Ranni",
        "Witch",
        "Elemer",
        "Undead Hunter",
        "nomadic",
        "merchant",
        "finger maiden",
        "puppet",

        // Creatures
        "dragon",
        "sentinel",

        // Locations
        "Limgrave",
        "Caelid",
        "Liurnia",
        "Leyndell",
        "Land of Reeds",
        "Badlands",
        "Altus Plateau",
        "church",
        "Siofra",
        "Ainsel",
        "Lake of Rot",
        "Nokron",
        "Mt. Gelmir",

        // Factions
        "giant",
        "Carian",
        "Caria",
        "Golden Lineage",
        "Vulgar militia",
        "Chrysalid",
        "Erdtree",
        "Ancestral",
        "Two Fingers",
        "Three Fingers",
        "Raya Lucaria",

        // Stats
        "strength",
        "intelligence",
        "faith",
        "mind",
        "endurance",
        "HP",

        // Abstracts
        "glintstone",
        "prince of death",
        "finger",
        "ruin",
        "moon"
      };
    }
  }
}