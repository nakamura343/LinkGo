using LinkGo.Common.Loader;
using UnityEngine;

public class TestLoader : MonoBehaviour
{
    private void Awake()
    {
        LoaderManager.Initialize(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        BaseLoader loader = LoaderManager.Load(ELoaderType.ResourceLoader, "Cube");
        loader.onCompleted += (type, obj)=>
        {
            Instantiate<GameObject>(obj as GameObject);
        };

        loader = LoaderManager.Load(ELoaderType.ResourceLoader, "Sphere");
        loader.onCompleted += (type, obj) =>
        {
            Instantiate<GameObject>(obj as GameObject);
        };

        for(int i = 0; i < 40; i++)
        {
            loader = LoaderManager.Load(ELoaderType.ResourceLoader, "Cylinder");
            loader.onCompleted += (type, obj) =>
            {
                GameObject go = Instantiate<GameObject>(obj as GameObject);
                go.name = string.Format("Cylinder{0}", i);
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoaderManager.Update();
    }
}
