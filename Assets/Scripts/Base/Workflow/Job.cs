using System;
using System.Threading.Tasks;

namespace LinkGo.Base.Workflow
{
    public class Job
    {
        private Task m_task;

        public int Id
        {
            get
            {
                if(m_task != null)
                {
                    return m_task.Id;
                }
                return 0;
            }
        }

        public bool IsCompleted
        {
            get
            {
                if (m_task != null)
                {
                    return m_task.IsCompleted;
                }
                return false;
            }
        }

        public Job(Action action)
        {
            m_task = Task.Factory.StartNew(action);
        }
    }
}