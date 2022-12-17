using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoap
{
 
        HashSet<KeyValuePair<string, object>> getWorldState();

        HashSet<KeyValuePair<string, object>> createGoalState();

        void planFailed(HashSet<KeyValuePair<string, object>> failedGoal);

        void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions);

        void actionsFinished();

        void planAborted(GoapAction aborter);

        bool moveAgent(GoapAction nextAction);


}
