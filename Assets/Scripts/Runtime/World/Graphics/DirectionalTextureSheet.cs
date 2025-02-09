using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [CreateAssetMenu(fileName = "DirectionalTextureSheet", menuName = "Hmlca/Gfx/DirectionalTextureSheet", order = 0)]
    public class DirectionalTextureSheet : ScriptableObject
    {
        private const int LENGTH = (int)Direction.Northwest + 1;


        [Tooltip("List of textures for each direction, starting from North and going clockwise.")]
        [SerializeField] private List<Texture2D> directionalTextures = new List<Texture2D>();


        public Texture2D this[int index] => directionalTextures[index];
        public Texture2D this[Direction direction] => this[(int) direction];


        private void Awake()
        {
            FillEmptyIndicesWithPlaceholder();
        }


        private void OnValidate()
        {
            FillEmptyIndicesWithPlaceholder();
        }


        private void FillEmptyIndicesWithPlaceholder()
        {
            while (directionalTextures.Count < LENGTH)
                directionalTextures.Add(null);
            for (int i = 0; i < LENGTH; i++)
            {
                if (directionalTextures[i] == null)
                    directionalTextures[i] = Resources.Load<Texture2D>("placeholder_000");
            }
        }
    }
}
