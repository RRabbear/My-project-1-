using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GamePlay2D
{
    public class CharacterBase : MonoBehaviour
    {
        Rigidbody2D _rigidbody;
        Collider2D _collider;

        PlusMultiAttr_Vector2 player_speed = new PlusMultiAttr_Vector2();

        public void LoadCharacterSetting()
        {

        }

        // Use this for initialization
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            player_speed.AddDect(new GetXYInputToVectorAttr());

            
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(player_speed.GetValue());

            _rigidbody.MovePosition(transform.position + new Vector3(player_speed.GetValue().x , player_speed.GetValue().y , 0) * 0.01f );
        }
    }
}