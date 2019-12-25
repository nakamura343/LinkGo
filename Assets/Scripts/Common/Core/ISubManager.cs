
namespace LinkGo.Common.Core
{
    public interface ISubManager
    {
        bool Init();

        void Uninit();

        void Update(float deltaTime);
    }
}


