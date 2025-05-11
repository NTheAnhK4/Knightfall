
using UnityEngine;



public class ESChaseData : EnemyPathFindingData
{
    
    public Transform Target;
   

}
public class ESChase : EnemyPathFinding
{

   


    private int _curretnWayPoint;
   

    private float _coolDown;
    
    public override void Enter(StateData stateData = null)
    {
        base.Enter(stateData);
        if (stateData != null && stateData is ESChaseData chaseData)
        {
            _target = chaseData.Target;

        }
       
    }
    

    public ESChase(EnemySM context) : base(context)
    {
    }
}
