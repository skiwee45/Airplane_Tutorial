using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;
        public float curSpeed;

    private void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime , Vector3.up);
          if(curSpeed != speed)
        {
            ChangeSpeed();
        }
    }
      private void ChangeSpeed()
    {
        if(curSpeed > speed)
        {
            curSpeed -=1;
        }
        else {
            curSpeed +=1;
        }
    }
}
