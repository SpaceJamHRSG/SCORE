using UnityEngine;

namespace Game
{
    public interface IUpgrade
    {
        public void Apply(PlayerManager player);
    }
}