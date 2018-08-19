using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public partial class NewEditModeTest {

    [SetUp]
    public void ResetScene() {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
    }

    public static SignIn CreateSignIn() {
        var obj = new GameObject();
        return obj.AddComponent<SignIn>();
    }

    SignIn GetInvalidSignIn() {
        var s = CreateSignIn();
        s.EmailInput.GetComponent<InputField>().text = "skyvr@sky.com";
        s.PasswordInput.GetComponent<InputField>().text = "zzzzzzz";
        return s;
    }

    SignIn GetValidSignIn() {
        var s = CreateSignIn();
        s.EmailInput.GetComponent<InputField>().text = "skyvr@sky.com";
        s.PasswordInput.GetComponent<InputField>().text = "v1rtualr3al!ty";
        return s;
    }

	[Test]
	public void TestSignInSuccessActiveInHierachy()
	{
	    var s = GetValidSignIn();	
        
        s.SignInButtonObj.GetComponent<Button>().onClick.Invoke();

	    Assert.IsTrue(s.SignInSuccess.activeInHierarchy);
    }

    [Test]
    public void TestSignInSuccessActiveSelf()
    {
        var s = GetValidSignIn();

        s.SignInButtonObj.GetComponent<Button>().onClick.Invoke();

        Assert.IsTrue(s.SignInSuccess.activeSelf);
    }

    [Test]
    public void TestSignInFailActiveInHierachy()
    {
        var s = GetInvalidSignIn();

        s.SignInButtonObj.GetComponent<Button>().onClick.Invoke();

        Assert.IsTrue(s.SignInFail.activeInHierarchy);
    }

    [Test]
    public void TestSignInFailActiveSelf()
    {
        var s = GetInvalidSignIn();

        s.SignInButtonObj.GetComponent<Button>().onClick.Invoke();

        Assert.IsTrue(s.SignInFail.activeSelf);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}


}
