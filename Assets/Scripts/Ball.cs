using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float force = 100f;
    public GameObject ballPrediction;
    public int maxTrajectoryIteration = 50;
    private Vector2 defaultBallPosition;
    private Vector2 startPosition;
    private Rigidbody2D rb;
    private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysics;
    private Scene scenePrediction;
    private PhysicsScene2D scenePredictionPhysics;
    public GameManager gameManager;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        defaultBallPosition = transform.position;
        CreateSceneMain();
        CreateScenePrediction();
    }

    void Update()
    {
        if (gameManager._isGameActive == false || GameManager.Instance.IsPointerOverUIElement())
        {
            startPosition = transform.position;
            GetComponent<LineRenderer>().positionCount = 0;
            return;
        }

        if (rb.velocity.magnitude <= 0.1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = GetMousePosition();
            }

            if (Input.GetMouseButton(0))
            {
                GameObject newBallPrediction = spawnBallPrediction();
                throwBall(newBallPrediction.GetComponent<Rigidbody2D>());
                CreateTrajectory(newBallPrediction);
                Destroy(newBallPrediction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<LineRenderer>().positionCount = 0;
                rb.simulated = true;
                throwBall(rb);
            }
        }
    }

    void FixedUpdate()
    {
        if (!sceneMainPhysics.IsValid()) return;
        sceneMainPhysics.Simulate(Time.fixedDeltaTime);
    }



    private void CreateTrajectory(GameObject newBallPrediction)
    {
        LineRenderer ballLine = GetComponent<LineRenderer>();
        ballLine.positionCount = maxTrajectoryIteration;
        for (int i = 0; i < maxTrajectoryIteration; i++)
        {
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
            ballLine.SetPosition(i,
                new Vector3(newBallPrediction.transform.position.x, newBallPrediction.transform.position.y, 0));
        }
    }

    private void throwBall(Rigidbody2D physics)
    {
        physics.AddForce(getThrowPower(startPosition, GetMousePosition()), ForceMode2D.Force);
    }

    private GameObject spawnBallPrediction()
    {
        GameObject newBallPrediction = GameObject.Instantiate(ballPrediction);
        SceneManager.MoveGameObjectToScene(newBallPrediction, scenePrediction);
        newBallPrediction.transform.position = transform.position;
        return newBallPrediction;
    }

    private Vector2 getThrowPower(Vector2 startPosition, Vector2 endPosition)
    {
        return (startPosition - endPosition) * force;
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CreateSceneMain()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
    }

    private void CreateScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }
}