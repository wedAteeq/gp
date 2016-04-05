using UnityEngine;
using UnityEngine.UI;

public class MenueCode : MonoBehaviour {
	public Transform HomePage;
	public Transform Login  ;
	public Transform CreateAccount ;
	public Transform ResetPassword;
	public Transform ForgetPassword;
	public Transform ContactUs;
	public Transform AdminView;
	public Transform memberView;
	public Transform Logout;
	public Transform notifications;
	public Transform addAdmin;
	public Transform membershipRe;
	public Transform ReserveAuction;
	public Transform AddContent;
	public Transform DeleteAccount;
	public Transform viewAccount;
	public Transform WelcomPage;
	public Transform Account1;
	public Transform Account2;
	public Transform ManegeAccount;
	public Transform ManageAdminAccount;
	public Transform ManegeMemberAccount;
	public Transform ConfirmDelete;
	public Transform ConfirmDeleteAdmin;
	public Transform ConfirmDeleteMember;
	public Transform manageContent;
	public Transform addContentRoom;
	public Transform DeleteContentRoom;
	public Transform  ArrangeContentRoom;
	public Transform UpdateAccount;
    public Transform viewFriendList;
    public Transform manageAdminAccount;
    public Transform Ncontent;
    public Transform Fcontent;
    public Animator GirlAvatar;
    public InputField UserName;
    public InputField Password;

    public InputField AdminUserName;
    public InputField AdminPassword;
    public InputField AdminConPassword;
    public InputField AdminEmail;
    public InputField AdminFirstName;
    public InputField AdminLastName;
    public InputField AdminBiography;

    public InputField MemberUserName;
    public InputField MemberPassword;
    public InputField MemberConPassword;
    public InputField MemberEmail;
    public InputField MemberFirstName;
    public InputField MemberLastName;
    public InputField MemberBiography;
    //public void LoadScene(string name){// to load the scene of the city
    //	Application.LoadLevel (name);}


    public void login(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			Login.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (false);

		} 
		else {
			Login.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}
       UserName.text = "";
        Password.text = "";
    }//end

	public void AddContentRoom(bool clicked)
	{ 
		if (clicked == true) {
			addContentRoom.gameObject.SetActive (clicked);
			manageContent.gameObject.SetActive (false);

		} 
		else {
			Login.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}
	}//end

    public void cancelAdmin(bool clicked)
    {
        if (clicked == true)
        {
            Account2.gameObject.SetActive(clicked);
            addAdmin.gameObject.SetActive(false);
            ManageAdminAccount.gameObject.SetActive(false);
			viewAccount.gameObject.SetActive(false);
			DeleteAccount.gameObject.SetActive(false);


        }
        else
        {
            Login.gameObject.SetActive(clicked);
            HomePage.gameObject.SetActive(true);
        }
    }//end



	public void cancelMember(bool clicked)
	{
		if (clicked == true)
		{
			Account1.gameObject.SetActive(clicked);

			ManegeMemberAccount.gameObject.SetActive(false);
			membershipRe.gameObject.SetActive(false);
			ReserveAuction.gameObject.SetActive(false);
		}
		else
		{
			Login.gameObject.SetActive(clicked);
			HomePage.gameObject.SetActive(true);
		}
	}//end


    public void ArrangeContentRoom1(bool clicked)
	{ 
		if (clicked == true) {
			ArrangeContentRoom.gameObject.SetActive (clicked);
			manageContent.gameObject.SetActive (false);

		} 
		else {
			Login.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}
	}//end

	public void DeleteContentRoom1(bool clicked)
	{ 
		if (clicked == true) {
			DeleteContentRoom.gameObject.SetActive (clicked);
			manageContent.gameObject.SetActive (false);

		} 
		else {
			Login.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}
	}//end


	public void Acoount1(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			Account1.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (false);

		} 
		else {
			Account1.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
		}
	}//end


	public void veiwmember(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			ManegeMemberAccount.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);

		} 
		else {
			Account1.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
		}
	}//end





	public void Acoount2(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			memberView.gameObject.SetActive (clicked);
			Account1.gameObject.SetActive (false);

		} 
		else {
			memberView.gameObject.SetActive (clicked);
			Account1.gameObject.SetActive (true);
		}
	}//end


	public void AccountAdmin(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			Account2.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (false);

		} 
		else {
			Account2.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
	}//end

	public void AccountAdmin1(bool clicked)//from home page to login form
	{ 
		if (clicked == true) {
			AdminView.gameObject.SetActive (clicked);
			Account2.gameObject.SetActive (false);

		} 
		else {
			AdminView.gameObject.SetActive (clicked);
			Account2.gameObject.SetActive (true);
		}
	}//end




	public void createAccount(bool clicked)////from home page to create account form
	{ 
		if (clicked == true) {
			CreateAccount.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (false);

		} 
		else {
			CreateAccount.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}

        MemberUserName.text = "";
        MemberPassword.text = "";
        MemberConPassword.text = "";
        MemberEmail.text = "";
        MemberBiography.text = "";
        MemberFirstName.text = "";
        MemberLastName.text = "";
    }//end

    public void createAccount1(bool clicked)////from home page to create account form
	{ 
		if (clicked == true) {
			CreateAccount.gameObject.SetActive (clicked);
			Login.gameObject.SetActive (false);
			HomePage.gameObject.SetActive (false);
		} 
		else {
			CreateAccount.gameObject.SetActive (clicked);
			Login.gameObject.SetActive (true);
		}
        MemberUserName.text = "";
        MemberPassword.text = "";
        MemberConPassword.text = "";
        MemberEmail.text = "";
        MemberBiography.text = "";
        MemberFirstName.text = "";
        MemberLastName.text = "";
    }//end


	public void ContactUs1(bool clicked)////from home page to contact us form
	{ 
		if (clicked == true) {
			ContactUs.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (false);

		} 
		else {
			ContactUs.gameObject.SetActive (clicked);
			HomePage.gameObject.SetActive (true);
		}
	}//end

	public void resetPassword(bool clicked)////from reset page to forget password form
	{ 
		if (clicked == true) {
			ResetPassword.gameObject.SetActive (clicked);
			ForgetPassword.gameObject.SetActive (false);

		} 
		else {
			ResetPassword.gameObject.SetActive (clicked);
			ForgetPassword.gameObject.SetActive (true);
		}
	}//end

	public void forgetPassword(bool clicked)////from login page to forget password form
	{ 
		if (clicked == true) {
			ForgetPassword.gameObject.SetActive (clicked);
			Login.gameObject.SetActive (false);

		} 
		else {
			ForgetPassword.gameObject.SetActive (clicked);
			Login.gameObject.SetActive (true);
		}
	}//end

	public void Logout1(bool clicked)//from Admin view to logout form
	{ 
		if (clicked == true) {
			Logout.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);

		} 
		else {
			Logout.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
	}//end


	public void Logout2(bool clicked)//from Member view to logout form
	{ 
		if (clicked == true) {
			Logout.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);

		} 
		else {
			Logout.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
		}
	}//end

    public void veiwFriend(bool clicked)//from home page to login form
    {
        if (clicked == true)
        {
            //viewFriendList.gameObject.SetActive (clicked);
            memberView.gameObject.SetActive(false);
            Account1.gameObject.SetActive(false);

        }

    }//end

    public void Logout3(bool clicked)// logout form
	{ 
		if (clicked == true) {
			Logout.gameObject.SetActive (clicked);
			ContactUs.gameObject.SetActive (false);
			addAdmin.gameObject.SetActive (false);
			AddContent.gameObject.SetActive (false);
			CreateAccount.gameObject.SetActive (false);
			DeleteAccount.gameObject.SetActive (false);
			ForgetPassword.gameObject.SetActive (false);
			viewAccount.gameObject.SetActive (false);
			membershipRe.gameObject.SetActive (false);
			ReserveAuction.gameObject.SetActive (false);
			WelcomPage.gameObject.SetActive (false);
			ManegeAccount.gameObject.SetActive (false);
			ManageAdminAccount.gameObject.SetActive (false);
			ManegeMemberAccount.gameObject.SetActive (false);
		} 
		else {
			Logout.gameObject.SetActive (clicked);
			ContactUs.gameObject.SetActive (true);
		}
	}//end

    public void notification(bool clicked)//from Member view to notification form
    {
        if (clicked == true)
        {
            //notifications.gameObject.SetActive (clicked);
            memberView.gameObject.SetActive(false);
            Account1.gameObject.SetActive(false);

        }
        else
        {
            //notifications.gameObject.SetActive (clicked);
            memberView.gameObject.SetActive(true);
        }
    }//end


    public void notification3(bool clicked)//from Admin view to notification form
	{ 
		if (clicked == true) {
			notifications.gameObject.SetActive (clicked);
			ContactUs.gameObject.SetActive (false);
			addAdmin.gameObject.SetActive (false);
			AddContent.gameObject.SetActive (false);
			CreateAccount.gameObject.SetActive (false);
			DeleteAccount.gameObject.SetActive (false);
			ForgetPassword.gameObject.SetActive (false);
			viewAccount.gameObject.SetActive (false);
			membershipRe.gameObject.SetActive (false);
			ReserveAuction.gameObject.SetActive (false);
			WelcomPage.gameObject.SetActive (false);
			ManegeAccount.gameObject.SetActive (false);
			ManageAdminAccount.gameObject.SetActive (false);
			ManegeMemberAccount.gameObject.SetActive (false);
		}
		else {
			notifications.gameObject.SetActive (clicked);
			ContactUs.gameObject.SetActive (true);
		}
		}//end
	

	public void addAdmin1(bool clicked)//from Admin view to add admin form
	{ 
		if (clicked == true) {
			addAdmin.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);

		} 
		else {
			addAdmin.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
        AdminUserName.text = "";
        AdminPassword.text = "";
        AdminConPassword.text = "";
        AdminEmail.text = "";
        AdminBiography.text = "";
        AdminFirstName.text = "";
        AdminLastName.text = "";
    }//end

	public void Request(bool clicked)//from member view to request membership form
	{ 
		if (clicked == true) {
			membershipRe.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);

		} 
		else {
			membershipRe.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
		}
	}//end

	public void reserve1(bool clicked)//from member view to request membership form
	{ 
		if (clicked == true) {
			ReserveAuction.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);

		

		} 
		else {
			ReserveAuction.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
		}
	}//end

	public void DeleteAccount1(bool clicked)//from Member view to logout form
	{ 
		if (clicked == true) {
			ConfirmDeleteMember.gameObject.SetActive (clicked);
			ManegeMemberAccount.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);


		} 
		else {
			ConfirmDelete.gameObject.SetActive (clicked);
			ManegeAccount.gameObject.SetActive (true);
		}
	}//end

	public void DeleteAccount3(bool clicked)//from Member view to logout form
	{ 
		if (clicked == true) {
			ConfirmDeleteAdmin.gameObject.SetActive (clicked);
			ManageAdminAccount.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);


		} 
		else {
			ConfirmDelete.gameObject.SetActive (clicked);
			ManegeAccount.gameObject.SetActive (true);
		}
	}//end

	public void DeleteAccount2(bool clicked)//from Member view to logout form
	{ 
		if (clicked == true) {
			DeleteAccount.gameObject.SetActive (clicked);

			ManageAdminAccount.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);

		} 
		else {
			ConfirmDelete.gameObject.SetActive (clicked);
			ManegeAccount.gameObject.SetActive (true);
		}
	}//end

	public void viewAccount1(bool clicked)//from Member view to logout form
	{ 
		if (clicked == true) {
			ManegeAccount.gameObject.SetActive (clicked);
			memberView.gameObject.SetActive (true);
			Account1.gameObject.SetActive (false);


		} 
		else {
			ManegeAccount.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
	}//end

	public void viewAccount2(bool clicked)
	{ 
		if (clicked == true) {
			viewAccount.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);

		} 
		else {
			viewAccount.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
	}//end

	public void Cancel(bool clicked)//from Admin view to add admin form
	{ 
		if (clicked == true) {
			HomePage.gameObject.SetActive (false);
			Logout.gameObject.SetActive (false);
			ConfirmDeleteAdmin.gameObject.SetActive (false);
		      	ArrangeContentRoom.gameObject.SetActive (false);
			    DeleteContentRoom.gameObject.SetActive (false);
			    addContentRoom.gameObject.SetActive (false);
				ContactUs.gameObject.SetActive (false);
				addAdmin.gameObject.SetActive (false);
				AddContent.gameObject.SetActive (false);
				CreateAccount.gameObject.SetActive (false);
				DeleteAccount.gameObject.SetActive (false);
				ForgetPassword.gameObject.SetActive (false);
				viewAccount.gameObject.SetActive (false);
				membershipRe.gameObject.SetActive (false);
				ReserveAuction.gameObject.SetActive (false);
				WelcomPage.gameObject.SetActive (false);
				ManegeAccount.gameObject.SetActive (false);
				ManageAdminAccount.gameObject.SetActive (false);
				ManegeMemberAccount.gameObject.SetActive (false);
			    ConfirmDeleteAdmin.gameObject.SetActive (false);
			    ConfirmDeleteMember.gameObject.SetActive (false);
		       	viewAccount.gameObject.SetActive (false);
			    addAdmin.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);
			Account2.gameObject.SetActive (false);

        }

        else {
			addAdmin.gameObject.SetActive (clicked);
			AdminView.gameObject.SetActive (true);
		}
	}//end

    public void Tour(bool clicked)
    {
        HomePage.gameObject.SetActive(false);
        GirlAvatar.gameObject.SetActive(true);

    }
    public void CancelCre(bool clicked)//from Admin view to add admin form
	{ 
		if (clicked == true) 

			HomePage.gameObject.SetActive (true);
			CreateAccount.gameObject.SetActive (false);
		    Login.gameObject.SetActive (false);


	}//end


    public void backToaccount1(bool clicked)//from Admin view to add admin form
    {
        if (clicked == true)
            if (Ncontent != null)
                foreach (Transform child in Ncontent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        if (Fcontent!= null)
        foreach (Transform child in Fcontent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Account1.gameObject.SetActive(true);
        ManegeMemberAccount.gameObject.SetActive(false);
        UpdateAccount.gameObject.SetActive(false);
        notifications.gameObject.SetActive(false);
        viewFriendList.gameObject.SetActive(false);
        notifications.gameObject.SetActive(false);

    }//end

    public void updateMember(bool clicked)//from Admin view to add admin form
	{ 
		if (clicked == true) 


			UpdateAccount.gameObject.SetActive (clicked);
		ManegeMemberAccount.gameObject.SetActive (false);
			Account1.gameObject.SetActive (false);
		ManegeMemberAccount.gameObject.SetActive (false);


	}//end

    public void backToaccount2(bool clicked)//from Admin view to add admin form
    {
        if (clicked == true)



            Account2.gameObject.SetActive(true);

        manageAdminAccount.gameObject.SetActive(false);
        notifications.gameObject.SetActive(false);



    }//end
    public void addcont(bool clicked)
	{ 
		if (clicked == true) {

			AddContent.gameObject.SetActive (clicked);
			ReserveAuction.gameObject.SetActive (false);
			//AdminView.gameObject.SetActive (true);
			Account2.gameObject.SetActive (false);
		} 
		else {
			AddContent.gameObject.SetActive (clicked);
			ReserveAuction.gameObject.SetActive (true);
		}
	}//end


}
