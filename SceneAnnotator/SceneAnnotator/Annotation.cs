using UnityEngine;

[System.Serializable]
public class Annotation
{
    public Vector3 Position;
    public string Message, Details;
    public Color Color;

    public Annotation(Vector3 position, string message, Color color, string details)
    {
        this.Position = position;
        this.Message = message;
        this.Color = color;
        this.Details = details;
    }
}
