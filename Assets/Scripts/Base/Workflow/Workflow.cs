using System;
using System.Collections.Generic;

namespace LinkGo.Base.Workflow
{
    public class Workflow
    {
        private Dictionary<int, Job> m_Jobs;

        public bool IsDone { private set; get; } = false;

        public float Progress
        {
            get
            {
                if(IsDone)
                {
                    return 1f;
                }
                int sum = m_Jobs.Count;
                int cur = 0;
                foreach(var job in m_Jobs)
                {
                    if(job.Value.IsCompleted)
                    {
                        cur++;
                    }
                }
                if(cur >= sum)
                {
                    IsDone = true;
                }
                return (sum > 0) ? ((float)cur / sum) : 0;
            }
        }

        public Workflow()
        {
            m_Jobs = new Dictionary<int, Job>();
        }

        public void AddJob(Action action)
        {
            Job job = new Job(action);
            m_Jobs.Add(job.Id, job);
        }

        public void RemoveJob(int id)
        {
            if(m_Jobs.ContainsKey(id))
            {
                m_Jobs.Remove(id);
            }
        }
    }
}


