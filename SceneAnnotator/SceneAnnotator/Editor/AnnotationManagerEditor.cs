using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnnotationManager))]
public class AnnotationManagerEditor : Editor
{
    private Vector3 handlePosition;
    private Color oldColor;


    // Method to draw in the Scene view
    private void OnSceneGUI()
    {
        AnnotationManager manager = (AnnotationManager)target;

        if (manager.annotationData != null)
        {
            foreach (var annotation in manager.annotationData.Annotations)
            {
                // Set handle color
                Handles.color = annotation.Color;

                // Calculate handle position above the annotation
                handlePosition = annotation.Position + Vector3.up * 1f;

                // Draw a sphere handle at the calculated position
                if (Handles.Button(handlePosition, Quaternion.identity, 0.5f, 0.5f, Handles.SphereHandleCap))
                {
                    // Button action, customize as needed
                    Debug.Log("Title: " + annotation.Message + "  Details:" + annotation.Details);
                }

                // Draw a line connecting the annotation to the handle
                Handles.DrawLine(annotation.Position, handlePosition, 2f);

                // Reset handle color
                Handles.color = oldColor;

                // Draw the background rectangle behind the text
                DrawTextBackground(annotation);

                // Draw the text with customized style
                Handles.Label(annotation.Position, annotation.Message, labelStyle());
            }
        }
    }


    // Method to get the label style
    private GUIStyle labelStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 18;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;

        return style;
    }


    // Method to draw the background rectangle behind the text
    private void DrawTextBackground(Annotation annotation)
    {
        // Get the label style and calculate text size
        Vector2 textSize = labelStyle().CalcSize(new GUIContent(annotation.Message));
        Vector2 padding = new Vector2(5, 5);

        // Calculate background rectangle position in screen coordinates
        Vector3 screenPos = HandleUtility.WorldToGUIPoint(annotation.Position);
        Rect rect = new Rect(screenPos.x - textSize.x / 2 - padding.x,
                             screenPos.y - textSize.y / 2 - padding.y,
                             textSize.x + padding.x * 2,
                             textSize.y + padding.y * 2);

        // Draw the background rectangle
        Handles.BeginGUI();
        oldColor = GUI.color;
        GUI.color = new Color(0, 0, 0, 0.5f);
        GUI.Box(rect, GUIContent.none);
        GUI.color = oldColor;
        Handles.EndGUI();

    }
}
