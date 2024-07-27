using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnnotationData", menuName = "SceneAnnotations/AnnotationData")]
public class AnnotationData : ScriptableObject
{
    public List<Annotation> Annotations = new List<Annotation>();
}
