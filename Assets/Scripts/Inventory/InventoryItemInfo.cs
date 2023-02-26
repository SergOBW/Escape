using New.Player.Absctract;
using UnityEngine;
namespace New.Player
{
    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Crate New ItemInfo")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxItemsInventorySlot;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _spriteIcon;
        [SerializeField] private GameObject _prefab;
        [SerializeField]private AnimatorOverrideController _overrideController;

        public string id { get => _id; }
        public int maxItemsInventorySlot { get => _maxItemsInventorySlot; }
        public string title { get => _title; }
        public string description { get => _description; }
        public Sprite spriteIcon { get => _spriteIcon; }
        public GameObject prefab { get => _prefab; }
        public AnimatorOverrideController overrideController
        {
            get => _overrideController;
        }
    }
    
}