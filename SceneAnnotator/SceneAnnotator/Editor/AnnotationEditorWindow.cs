using UnityEditor;
using UnityEngine;

public class AnnotationEditorWindow : EditorWindow
{
    private AnnotationData annotationData; // Reference to the AnnotationData Scriptable Object
    private Vector3 newAnnotationPosition; // Position for the new annotation
    private string newAnnotationMessage; // Message for the new annotation
    private string newAnnotationDetails; // Details for the new annotation
    private Color newAnnotationColor = Color.white; // Color for the new annotation


    // Add a menu item to show the AnnotationEditorWindow
    [MenuItem("Window/SceneAnnotations")]
    public static void ShowWindow()
    {
        GetWindow<AnnotationEditorWindow>("Scene Annotations");
    }


    // OnGUI is called to draw and handle the GUI events
    private void OnGUI()
    {
        // Draw a bold label for the annotation data section
        GUILayout.Label("Annotation Data", EditorStyles.boldLabel);

        // Object field to assign the AnnotationData asset
        annotationData = (AnnotationData)EditorGUILayout.ObjectField("Annotation Data",
                                                                     annotationData,
                                                                     typeof(AnnotationData),
                                                                     false);

        if (annotationData != null)
        {
            GUILayout.Space(10);

            // Section for adding a new annotation
            GUILayout.Label("Add New Annotation", EditorStyles.boldLabel);
            newAnnotationPosition = EditorGUILayout.Vector3Field("Position", newAnnotationPosition);
            newAnnotationMessage = EditorGUILayout.TextField("Message", newAnnotationMessage);
            newAnnotationColor = EditorGUILayout.ColorField("Color", newAnnotationColor);
            newAnnotationDetails = EditorGUILayout.TextField("Details", newAnnotationDetails);

            // Button to add a new annotation
            if (GUILayout.Button("AddAnnotation"))
            {
                AddAnnotation();
            }

            GUILayout.Space(10);

            // Section for displaying existing annotations
            GUILayout.Label("Existing Annotations", EditorStyles.boldLabel);
            foreach (var annotation in annotationData.Annotations)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(annotation.Message);

                // Button to remove an annotation
                if (GUILayout.Button("Remove"))
                {
                    RemoveAnnotation(annotation);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void AddAnnotation()
    {
        // Add a new annotation with the specified properties
        annotationData.Annotations.Add(new Annotation(newAnnotationPosition,
                                                      newAnnotationMessage,
                                                      newAnnotationColor,
                                                      newAnnotationDetails));

        // Mark the AnnotationData asset as dirty to ensure the changes are saved        
        EditorUtility.SetDirty(annotationData);
    }

    private void RemoveAnnotation(Annotation annotation)
    {
        // Remove the specified annotation
        annotationData.Annotations.Remove(annotation);

        // Mark the AnnotationData asset as dirty to ensure the changes are saved        
        EditorUtility.SetDirty(annotationData);
    }
}
