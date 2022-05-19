using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tesco.Managers;
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
    public EnemyManager e_Manager;
    public GameManager manager;
    public getTurn turn;

    public NavMeshAgent agent;
    public Rigidbody rb;

    public float speed;
    public LineRenderer line;
    public float limit;
    public List<Vector3> pathList;

    public int randXValue;
    public float angle;
    [SerializeField] private float m_AIRotSpeed = 5;

    public float rayLength;
    public float groundDist;

    [Space(10)]
    public float setAngle = 45f;

    public float gravity = 9.81f;
    [SerializeField] private float Range;
    public float RequiredVelocity;

    public bool opponentTurnStarted;
    public bool shot;

    public GameObject hitObj;

    public float hitRange;
    public Transform target;
    public bool reachedNearTheTarget;

    // Once completed, it would be ready to set turns for it: for this
    // it will already have the number of points in the list. Now everytime a bool Turn is on, it will perform a shoot towards a direction and delete that point from the list

    private IEnumerator Init()
    {
        while (e_Manager == null && manager == null)
        {
            yield return null;
        }

        shot = false;
        destination = e_Manager.destination;
        agent = GetComponent<NavMeshAgent>();
        turn = GetComponent<getTurn>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        reachedNearTheTarget = false;
        agent.enabled = true;
        rb.drag = 100000;
        rb.angularDrag = 50;
        agent.isStopped = true;
        agent.ResetPath();

        StartCoroutine(Init());
    }

    private void Update()
    {
        if (destination != null && turn.myTurn)
        {
            RaycastHit hit;

            Vector3 dir = destination.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("destination"))
                {
                    reachedNearTheTarget = true;
                }
            }

            if (reachedNearTheTarget)
            {
                Debug.Log("Hit name: " + hit.collider.gameObject.name);
                rb.AddForce(dir * 20.0f, ForceMode.Impulse);
            }
            else
            {
                if (!opponentTurnStarted)
                {
                    opponentTurnStarted = true;
                    manager.ChangeTurn();
                }

                if (opponentTurnStarted)
                {
                    opponentTurnStarted = false;

                    if (agent.isOnNavMesh)
                    {
                        agent.isStopped = true;
                    }

                    StartCoroutine(LookAtTheDestination());
                }
            }
        }
    }

    public void GetPathCorners()
    {
        if (agent.enabled)
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
    }

    public IEnumerator LookAtTheDestination()
    {
        GetPathCorners();
        if (pathList.Count > 0)
        {
            Vector3 target = pathList[1];

            while ((angle = UtilityClass.AngleBetweenObjects(transform.position, target, transform.forward)) >= 10)
            {
                Quaternion lookRot = UtilityClass.RotateTowardsTargetUsingSlerp(target, this.gameObject);
                lookRot.x = 0.0f;
                lookRot.z = 0.0f;

                transform.rotation = lookRot;

                yield return null;
            }

            Shoot();

            yield return new WaitForSeconds(8);

            StartCoroutine(Cooldown());
        }
    }

    public void Shoot()
    {
        Range = Vector3.Distance(pathList[1], transform.position);
        RequiredVelocity = Mathf.Sqrt((Range * gravity) / Mathf.Sin(2 * setAngle));
        rb.drag = 0;
        rb.angularDrag = 0.05f;
        agent.enabled = false;
        rb.velocity = (transform.forward + transform.up) * RequiredVelocity;
        shot = true;
    }

    public IEnumerator Cooldown()
    {
        while (true)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.collider.CompareTag("ground"))
                {
                    float dist = Vector3.Distance(transform.position, hit.collider.transform.position);
                    Debug.Log($"Distance between the ball and the {hit.collider.gameObject.name}: " + dist);
                    if (dist <= groundDist)
                    {
                        Debug.Log("Reset sphere started");
                        StartCoroutine(ResetSphere());
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    public IEnumerator ResetSphere()
    {
        while (rb.velocity.sqrMagnitude >= 5f)
        {
            Debug.Log("Mag: " + rb.velocity.sqrMagnitude);
            yield return null;
        }

        if (rb.velocity.sqrMagnitude <= 5f)
        {
            Debug.Log("Mag: " + rb.velocity.sqrMagnitude);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        agent.enabled = true;
        rb.drag = 100000;
        rb.angularDrag = 50;
        agent.isStopped = true;
        agent.ResetPath();

        yield return null;
    }
}