using UnityEngine;

namespace LinkGo.Common.Profiler
{
    public class EasyFps : MonoBehaviour
    {
        public bool ncm;
        public int maxFR;
        public float refresht = 0.5f;
        int frameCounter = 0;
        float timeCounter = 0.0f;
        float lastFramerate = 0.0f;

        bool acttxt = true;
        [SerializeField]
        public float FPS
        {
            get { return lastFramerate; }
        }

        [SerializeField]
        public float RefreshTime
        {
            get { return refresht; }
            set { refresht = value; }
        }

        int mx = 60;
        public int MaxFrameRate
        {
            get { return mx; }
            set { mx = value; Application.targetFrameRate = value; }
        }

        void Start()
        {
            if (ncm == true)
            {
                mx = maxFR;
                RefreshTime = refresht;
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = maxFR;
            }
        }

        void Update()
        {
            if (timeCounter < refresht)
            {
                timeCounter += Time.deltaTime;
                frameCounter++;
            }
            else
            {
                lastFramerate = (float)frameCounter / timeCounter;
                int lastfrInt = (int)lastFramerate;

                if (acttxt == true)
                {
                    if (lastFramerate <= MaxFrameRate)
                    {
                       
                    }
                    else
                    {

                    }
                }
                frameCounter = 0;
                timeCounter = 0.0f;
            }
        }
    }
}


