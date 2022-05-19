using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesco.Managers
{
    // Dispatches game events to listeners
    public class EventManager
    {
        public static Action<Movement> ballScoreEvent;
        public static Action<Movement> ballStrokedOutEvent;
        public static Action resetBallsEvent;
        public static Action<Movement> ballHitEvent;
        public static Action<Movement> checkStrokeCountEvent;
        public static Action<Movement> changePlayerTurnEvent;
        public static Action<Collectible> pickupCollectedEvent;
        public static Action onMultiplayerMode;     // this is to detect whether the game mode is a multiplayer and if so, than the turn system is set up

        // Fired when player has reached stroke limit for course
        public static void StrokeOut(Movement movement)
        {
            ballStrokedOutEvent?.Invoke(movement);
        }

        // Fired when a turn ends and the current player changes (???)
        public static void ChangeTurn(Movement player)
        {
            changePlayerTurnEvent?.Invoke(player);
        }

        // Fired when the ball stops moving after a hit
        public static void CheckStrokes(Movement movement)
        {
            checkStrokeCountEvent?.Invoke(movement);
        }

        // Fired when the ball is hit
        public static void HitBall(Movement movement)
        {
            ballHitEvent?.Invoke(movement);
        }

        // Fired when the ball should be reset when moving to a new hole on the same course
        public static void ResetBalls()
        {
            resetBallsEvent?.Invoke();
        }

        // Fired when a player lands a ball in the hole
        public static void BallScored(Movement move)
        {
            ballScoreEvent?.Invoke(move);
        }

        // Fired when a player collects a pickup
        public static void PickupCollected(Collectible collectible)
        {
            pickupCollectedEvent?.Invoke(collectible);
        }
    }
}