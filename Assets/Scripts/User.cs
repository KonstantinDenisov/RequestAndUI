using Newtonsoft.Json;

public class User
{
    [JsonProperty ("Username")]
    public string Username;
    [JsonProperty ("AvatarUrl")]
    public string AvatarUrl;
    [JsonProperty ("Points")]
    public int Points;
    
    public override string ToString() =>
        $"Username '{Username}', AvatarUrl '{AvatarUrl}', Points '{Points}'";
}