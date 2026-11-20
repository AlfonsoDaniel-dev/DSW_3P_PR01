namespace dsw_3p_pr01.Models;

using System.Text.Json.Serialization;

public class Mission {

    public int Id {get; set;}

    [JsonPropertyName("nombre")]
    public string Name {get; set;} = "";

    [JsonPropertyName("dificultad")]
    public string Difficulty {get; set;} = "Media";

    [JsonPropertyName("recompensaXP")]
    public int ExperienceReward {get;set;}

    [JsonPropertyName("completada")]
    public bool completed {get;set;}

}
