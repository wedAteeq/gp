using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using UnityEngine.UI;
using Sfs2X.Util;
using UnityEngine.EventSystems;
public class ReserveAuction : MonoBehaviour
{

	//----------------------------------------------------------
	// Private properties (Connection part)
	//----------------------------------------------------------
	private string ServerIP = "127.0.0.1";
// Default host
	private int defaultTcpPort = 9933;
// Default TCP port
	private int defaultWsPort = 8888;
	// Default WebSocket port
	private string ZoneName = "3DexCityZone";
	private int ServerPort = 0;

	private SmartFox sfs;
	private string username;
	private string aucName;
	private string aucType;
	private string card;
	private string cardEndMonth;
	private string cardEndYear;
	private string aucDate;
	private string aucTime;
	private string items;
	private string slots;
	private int Error = 0;
	private int room = 0;

	//----------------------------------------------------------
	// UI elements
	//----------------------------------------------------------

	public InputField AucName;
	public InputField AucType;
	public InputField Card;
	public InputField CardEndMonth;
	public InputField CardEndYear;
	public Transform ChooseTime;
	public Transform ReserveAuctionForm;


	string CMD_Signup = "$SignUp.Submit";
//-----------------


	// Use this for initialization
	void Start ()
	{
		//TextMessage.text = "";
	}

	// Update is called once per frame
	void Update ()
	{
		// As Unity is not thread safe, we process the queued up callbacks on every frame
		if (sfs != null)
			sfs.ProcessEvents ();
	}

	//----------------------------------------------------------
	// Public interface methods for UI
	//----------------------------------------------------------
	public void OnChooseTimeButtonClicked ()
	{
		
		//getDay
		//dinamkly disable buttens and change their colors
		//display chhose thime interface and hide the other interfaces
		ReserveAuctionForm.gameObject.SetActive(false);
		ChooseTime.gameObject.SetActive(true);

	}
	//end ChooseTimeButton
	public void OnTimeButtonClicked ()
	{
		Debug.Log ("true");
		//less than six buttons
		if (slots.Length == 5) {
		//display message
			return;
		}
		//consequntive
		string selected=EventSystem.current.currentSelectedGameObject.name;
	
		//int selectedNum = selected;
		//if (slots.Contains((selectedNum+1)+""))
		//	selectedNum = 4;
		//get last choosed and check if it greater than it

	}
//end TimeButton


}
