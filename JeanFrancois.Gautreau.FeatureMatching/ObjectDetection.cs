using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeanFrancois.Gautreau.FeatureMatching;

public record ObjectDetection()
{
    public IList<ObjectDetectionResult> DetectObjectInScenes(byte[] objectImageData, IList<byte[]> imagesSceneData)
    {
        var tasks = new List<Task>();
        var result = new List<ObjectDetectionResult>();
        foreach (var imageSceneData in imagesSceneData)
        {
            var task = Task.Run(() =>
                result.Add(DetectObjectInScenes(objectImageData, imageSceneData)));
            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray());
        return tasks.Select(task => ((Task<ObjectDetectionResult>) task).Result).ToList();
    }
}