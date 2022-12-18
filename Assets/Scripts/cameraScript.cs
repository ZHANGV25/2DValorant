using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Material GLDraw;
    GameObject player;
    float camHeight, camWidth;
    Camera cam;

    [SerializeField] private Vector2 shadowOffset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        transform.position = pos;
    }

    void OnPostRender()
    {
        float cameraLeft = transform.position.x - camWidth / 2;
        float cameraBottom = transform.position.y - camHeight / 2;
        


        GL.PushMatrix();
        GLDraw.SetPass(0);
        GL.LoadOrtho();

        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, new Vector2(camWidth, camHeight), 0, LayerMask.GetMask("Wall"));
        foreach (Collider2D collision in collisions)
        {
            wall wall = collision.GetComponent<wall>();
            float left = (wall.left - cameraLeft - shadowOffset.x) / camWidth;
            float top = (wall.top - cameraBottom + shadowOffset.y) / camHeight;
            float right = (wall.right - cameraLeft - shadowOffset.x) / camWidth;
            float bottom = (wall.bottom - cameraBottom + shadowOffset.y) / camHeight;

            if (player.transform.position.x <= wall.centerX && player.transform.position.y <= wall.centerY) DrawShadow(left, bottom, right, top);
            if (player.transform.position.x <= wall.centerX && player.transform.position.y >= wall.centerY) DrawShadow(left, top, right, bottom);
            if (player.transform.position.x >= wall.centerX && player.transform.position.y >= wall.centerY) DrawShadow(right, top, left, bottom);
            if (player.transform.position.x >= wall.centerX && player.transform.position.y <= wall.centerY) DrawShadow(right, bottom, left, top);
        }

        GL.PopMatrix();
    }

    void DrawShadow(float x1, float y1, float x2, float y2)
    {
        float x = 0.5f;
        float y = 0.5f;
        int projectedLength = 100;
        float projx1 = x2 + (x2 - x) * projectedLength;
        float projy1 = y1 + (y1 - y) * projectedLength;
        float projx2 = x1 + (x1 - x) * projectedLength;
        float projy2 = y2 + (y2 - y) * projectedLength;

        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.black);

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x2, y1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x1, y2, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.End();
    }
}
