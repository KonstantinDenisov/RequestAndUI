using Newtonsoft.Json;
using UnityEngine;

public class User
{
    [JsonProperty ("Username")]
    public string Username;
    [JsonProperty ("AvatarUrl")]
    public string AvatarUrl;
    [JsonProperty ("Points")]
    public int Points;

    public Sprite AvatarSprite;
    
    public override string ToString() =>
        $"Username '{Username}', AvatarUrl '{AvatarUrl}', Points '{Points}'";
}