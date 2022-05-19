using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Utility;

public enum OpponentState
{ AIM, WAIT, SHOOT };

/* AI steps
 * 1. Set the reference to the destination -> Completed
 * 2. Get the nextPosition ->
 * 3. calculate the distance from the enemy position to the nextPosition
 * 4. Choose a random value within the distance
 * 5. Shoot in that position
 *
 *
 * for the AI
 * check for the 1st point of turn on the navmesh
 * shoot a raycast
 * check the hit point
 * calculate the distance
 * turn towards the point
 * shoot
 *
 */

public class OpponentAI : MonoBehaviour
{
    public GameObject arrow;
    public GameObject destination;
    public Ray ray;
    public RaycastHit hit;
    public NavMeshAgent agent;
    public Rigidbody rb;

    public float speed;
    public LineRenderer line;
    public float limit;
    public List<Vector3> pathList;

    public int randXValue;
    public float angle;
    [SerializeField] private float m_AIRotSpeed = 5;

    [Space(10)]
    public float setAngle = 45f;

    public float gravity = 9.81f;
    [SerializeField] private float Range;
    public float RequiredVelocity;

    public bool opponentTurnStarted;

    // Once completed, it would be ready to set turns for it: for this
    // it will already have the number of points in the list. Now everytime a bool Turn is on, it will perform a shoot towards a direction and delete that point from the list

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !opponentTurnStarted)
        {
            opponentTurnStarted = true;
        }

        if (opponentTurnStarted)
        {
            opponentTurnStarted = false;

            ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

            if (agent.isOnNavMesh)
            {
                agent.isStopped = true;
            }

            StartCoroutine(LookAtTheDestination());
        }
    }

    public void GetPathCorners()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination.transform.position, path);
        pathList = path.corners.ToList();

        // remove all the corners whose distance is less than the certain limit
        foreach (var item in path.corners)
        {
            if (Vector3.Distance(agent.transform.position, item) >= limit)
            {
                pathList.Add(item);
            }
        }

        // set the line positions
        UtilityClass.LineRenderer(this.gameObject, destination.transform, pathList);
    }

    public IEnumerator LookAtTheDestination()
    {
        GetPathCorners();
        Vector3 toTarget = pathList[1] - transform.position;

        while ((angle = Vector3.Angle(transform.forward, toTarget)) >= 2f)
        {
            Debug.Log("Gone into the 1st while");

            transform.rotation = Quaternion.LookRotation(
                                                    Vector3.MoveTowards(transform.forward, toTarget, m_AIRotSpeed * Time.deltaTime),
                                                    Vector3.up);
            yield return null;
        }

        Range = Vector3.Distance(pathList[1], transform.position);
        RequiredVelocity = Mathf.Sqrt((Range * gravity) / Mathf.Sin(2 * setAngle));
        rb.drag = 0;
        rb.angularDrag = 0.05f;
        agent.enabled = false;
        rb.velocity = (transform.forward + transform.up) * RequiredVelocity;

        yield return new WaitForSeconds(2);
        while (true)
        {
            if ((agent.transform.position.y - agent.baseOffset) < 0f)
            {
                StartCoroutine(ResetSphere());

                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator ResetSphere()
    {
        while (rb.velocity.magnitude < 0.4f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        agent.enabled = true;
        rb.drag = 100000;
        rb.angularDrag = 50;
        agent.isStopped = true;
        agent.ResetPath();
    }
}