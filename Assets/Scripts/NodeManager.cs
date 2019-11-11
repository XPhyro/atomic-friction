using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Extension.Native;

public class NodeManager : MonoBehaviour
{
	private const uint StaticNodeCount = 20;

	public static NodeManager Singleton; 
	
	public static List<StaticNode> StaticNodes = new List<StaticNode>();
	public static List<MovingNode> MovingNodes = new List<MovingNode>();
	public static List<Node> Nodes = new List<Node>();

	[SerializeField] 
	private GameObject staticNodePrefab;
	[SerializeField] 
	private GameObject movingNodePrefab;

	private void Awake()
	{
		if (Singleton)
		{
			Destroy(this);
		}
		else
		{
			Singleton = this;
		}
	}

	public void Initialise()
	{
		InitialiseStaticNodes();
		InitialiseMovingNode();
	}

	private void InitialiseStaticNodes()
	{
		var refGo = staticNodePrefab;
		var gos = new List<GameObject>();

		//create static nodes in a line
		for(var i = 0; i < StaticNodeCount; i++)
		{
			var t = transform;
			var pos = (t.position + Vector3.right * (i - StaticNodeCount / 2f))
			          .Scale(2, 1, 1) + Vector3.right;
			var go = Instantiate(refGo, pos, Quaternion.identity, t);
			gos.Add(go);
		}

		StaticNodes = gos.Select(go => go.GetComponent<StaticNode>()).ToList();
		Nodes.AddRange(StaticNodes);
	}

	private void InitialiseMovingNode()
	{
		var t = transform;
		var pos = (t.position + Vector3.left * (StaticNodeCount / 2f))
		          .Scale(2, 1, 1) + Vector3.right + 2 * Vector3.up;
		var go = Instantiate(movingNodePrefab, pos, Quaternion.identity, t);
		var movingNode = go.GetComponent<MovingNode>();
		
		MovingNodes.Add(movingNode);
		Nodes.Add(movingNode);
	}
}