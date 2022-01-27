using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesco.Managers {

    // Dispatches game events to listeners
    public class EventManager {

        public static Action<Movement> ballScoreEvent;
        public static Action<Movement> ballStrokedOutEvent;
        public static Action resetBallsEvent;
        public static Action<Movement> ballHitEvent;
        public static Action<Movement> checkStrokeCountEvent;
        public static Action<Movement> changePlayerTurnEvent;

        // Fired when the player's strokes exceed the current hole's amount. (Fired in CourseController)
        public static void StrokeOut(Movement movement) {
            if(ballStrokedOutEvent != null) {
                ballStrokedOutEvent.Invoke(movement);
            }
        }

        // Fired when the current player's ball stops moving to tell the next player it's their turn.
        public static void ChangeTurn(Movement player)
        {
            if(changePlayerTurnEvent != null)
            {
                changePlayerTurnEvent.Invoke(player);
            }
        }

        // Fired when the ball stops moving after a hit
        public static void CheckStrokes(Movement movement) {
            if(checkStrokeCountEvent != null) {
                checkStrokeCountEvent.Invoke(movement);
            }
        }

        // Fired when the ball is hit
        public static void HitBall(Movement movement) {
            if(ballHitEvent != null) {
                ballHitEvent.Invoke(movement);
            }
        }

        // Fired when the ball should be reset during a same-course hole transition
        public static void ResetBalls() {
            if(resetBallsEvent != null) {
                resetBallsEvent.Invoke();
            }
        }

        // Fired when a player lands a ball in the hole
        public static void BallScored(Movement move) {
            if(ballScoreEvent != null) {
                ballScoreEvent.Invoke(move);
            }
        }
    }
}
