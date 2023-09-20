// using System;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// public class Ball : MonoBehaviour
// {
//     public float force = 100f;
//     private Vector2 _startPosition;
//     private Vector2 _endPosition;
//     private Vector2 defaultPosition;
//     private Rigidbody2D _rb;
//     private Scene sceneMain;
//     private Scene scenePredict;
//     private PhysicsScene2D sceneMainPhysics;
//     private PhysicsScene2D scenePredictPhysics;
//     public GameObject ballPrediction;
//     public int trajetory = 50;
//     private void Awake()
//     {
//         _rb = GetComponent<Rigidbody2D>();
//     }
//     void Start()
//     {
//         _rb.isKinematic = true;
//         Physics2D.simulationMode = SimulationMode2D.Script;
//         defaultPosition = transform.position;
//         
//         sceneMain = SceneManager.CreateScene("MainScene");
//         sceneMainPhysics = sceneMain.GetPhysicsScene2D();
//         CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
//         scenePredict = SceneManager.CreateScene("PredictScene", sceneParameters);
//         scenePredictPhysics = scenePredict.GetPhysicsScene2D();
//     }
//     
//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//              _startPosition = GetMousePosition();
//         }
//         if (Input.GetMouseButton(0))
//         {
//           Vector2 dragPosition = GetMousePosition();
//           Vector2 power = _startPosition - dragPosition;
//
//           GameObject prediction = GameObject.Instantiate(ballPrediction);
//           SceneManager.MoveGameObjectToScene(prediction, scenePredict);
//           
//           prediction.transform.position = transform.position;
//           prediction.GetComponent<Rigidbody2D>().AddForce(power, ForceMode2D.Impulse);
//           createTrajetoery(prediction);
//           Destroy(prediction);
//         }
//         if (Input.GetMouseButtonUp(0))
//         {
//             GetComponent<LineRenderer>().positionCount = 0;
//             _rb.AddForce(ThrowPower(_startPosition,GetMousePosition()) , ForceMode2D.Force);
//            _rb.isKinematic = false;
//         }
//     }
//
//     private void createTrajetoery(GameObject prediction)
//     {
//         LineRenderer lineRenderer = GetComponent<LineRenderer>();
//         lineRenderer.positionCount = trajetory;
//         for (int i = 0; i < 50; i++)
//         {
//             scenePredictPhysics.Simulate(Time.fixedDeltaTime);
//             lineRenderer.SetPosition(i, new Vector3(prediction.transform.position.x, prediction.transform.position.y, 0));
//         }
//     }
//
//     private void FixedUpdate()
//     {
//         if (!sceneMainPhysics.IsValid()) return;
//         sceneMainPhysics.Simulate(Time.fixedDeltaTime);
//     }
//
//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         _rb.isKinematic = true;
//         transform.position = defaultPosition;
//         _rb.velocity = Vector2.zero;
//         _rb.angularVelocity = 0f;
//     }
//     private Vector2 ThrowPower(Vector2 startPosition, Vector2 endPosition)
//     {
//         return (startPosition - endPosition)* force;
//     }
//     private Vector2 GetMousePosition()
//     {
//         return Camera.main.ScreenToWorldPoint(Input.mousePosition);
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
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
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        rb.isKinematic = true;
        defaultBallPosition = transform.position;
        createSceneMain();
        createScenePrediction();                
    }
    void Update()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = getMousePosition();
            }

            if (Input.GetMouseButton(0))
            {
                GameObject newBallPrediction = spawnBallPrediction();
                throwBall(newBallPrediction.GetComponent<Rigidbody2D>());
                createTrajectory(newBallPrediction);
                Destroy(newBallPrediction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<LineRenderer>().positionCount = 0;
                rb.isKinematic = false;
                throwBall(rb);
            }
        }
    }
    void FixedUpdate()
    {
        if (!sceneMainPhysics.IsValid()) return;
        sceneMainPhysics.Simulate(Time.fixedDeltaTime);
    }

    private void createTrajectory(GameObject newBallPrediction){
        LineRenderer ballLine = GetComponent<LineRenderer>();
        ballLine.positionCount = maxTrajectoryIteration;
        for (int i = 0; i < maxTrajectoryIteration; i++)
        {
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
            ballLine.SetPosition(i, new Vector3(newBallPrediction.transform.position.x, newBallPrediction.transform.position.y, 0));
        }
    }
    private void throwBall(Rigidbody2D physics){
        physics.AddForce(getThrowPower(startPosition, getMousePosition()), ForceMode2D.Force);
    }
    private GameObject spawnBallPrediction(){
        GameObject newBallPrediction = GameObject.Instantiate(ballPrediction);
        SceneManager.MoveGameObjectToScene(newBallPrediction, scenePrediction);
        newBallPrediction.transform.position = transform.position;
        return newBallPrediction;
    }
    private Vector2 getThrowPower(Vector2 startPosition, Vector2 endPosition){
        return (startPosition - endPosition) * force;
    }
    private Vector2 getMousePosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void createSceneMain()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
    }
    private void createScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }
    
}