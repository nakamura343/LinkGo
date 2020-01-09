using LinkGo.Base.IO;
using LinkGo.Base.Workflow;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestCopyAsync : MonoBehaviour
{
    public Slider slider;

    Workflow workflow;

    // Start is called before the first frame update
    void Start()
    {
        string dir = "F:/Documents/math";
        string output = "F:/math2";

        workflow = new Workflow();

        List<string> files = FileHelper.GetFiles(dir);

        foreach(var file in files)
        {
            workflow.AddJob(() =>
            {
                CopyAsync(file, output);
            });
        }
    }

    void CopyAsync(string path, string output)
    {

        FileInfo source = new FileInfo(path);
        if(Directory.Exists(output) == false)
        {
            Directory.CreateDirectory(output);
        }

        string target = string.Format("{0}/{1}", output, source.Name);
        using (FileStream s = File.OpenRead(path))
        {
            using (FileStream streamWriter = File.Create(target))
            {
                int size = 2048;
                byte[] data = new byte[2048];
                while (true)
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        streamWriter.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!workflow.IsDone)
        {
            slider.value = workflow.Progress;
        }
        else
        {
            slider.value = 1f;
        }
    }
}
