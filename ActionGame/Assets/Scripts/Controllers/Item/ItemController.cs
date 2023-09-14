using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    PlayerController _player;
    Define.DropItem _type;

    public void SetInfo(Define.DropItem type)
    {
        if(type != Define.DropItem.Big_Coin || type != Define.DropItem.Small_Coin)
        {
            _player = Managers.Object.Player;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(_type)
            {
                case Define.DropItem.Hp_Heal:
                    _player.Hp += 10f;
                    if (_player.Hp >= _player.MaxHp)
                        _player.Hp = _player.MaxHp;
                    break;
                case Define.DropItem.Mp_Heal:
                    _player.Mp += 10f;
                    if (_player.Mp >= _player.MaxMp)
                        _player.Mp = _player.MaxMp;
                    break;
                case Define.DropItem.Big_Coin:
                    Managers.Game.CurrentGold += 5;
                    break;
                case Define.DropItem.Small_Coin:
                    Managers.Game.CurrentGold += 1;
                    break;
                case Define.DropItem.Shield:
                    GameObject shield = _player.gameObject.FindChild("Shield");
                    if (shield != null)
                        Managers.Resource.Destory(shield);

                    Managers.Resource.Instantiate("Item/ShieldEffect", _player.transform);
                    break;
            }
        }
    }

    void OnUseItem()
    {
        
    }
}
