using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    public GameObject EmailInput;
    public GameObject PasswordInput;
    public GameObject SignInButtonObj;

    public GameObject SignInPage;

    public GameObject SignInSuccess;

    public GameObject SignInFail;

    string _email;
    string _password;
    object _authServerLastSuccessResponse;

    InputField _emailInputField;
    InputField EmailInputField {
        get {
            if (_emailInputField == null)
                _emailInputField = EmailInput.GetComponent<InputField>();
            return _emailInputField;
        }
    }

    InputField _passwordInputField;
    InputField PasswordInputField {
        get {
            if (_passwordInputField == null)
                _passwordInputField = PasswordInput.GetComponent<InputField>();
            return _passwordInputField;
        }
    }

    Button _signInButton;
    Button SignInButton {
        get {
            if (_signInButton == null)
                _signInButton = SignInButtonObj.GetComponent<Button>();
            return _signInButton;
        }
    }

    public Text FailReasonText;

    // Use this for initialization
    void Start() {
        EmailInputField.onValueChanged.AddListener(e => _email = e);
        PasswordInputField.onValueChanged.AddListener(p => _password = p);
        SignInButton.onClick.AddListener(OnSignIn);
    }

    // Update is called once per frame
    void Update() {

        /* check for key presses to navigate the page */

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (_emailInputField.isFocused)
                PasswordInputField.Select();

            if (PasswordInputField.isFocused)
                _emailInputField.Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
            OnSignIn();
    }

    void OnSignIn() {
        /* if we were sending over a network we would encrypt the user details */
        Server.SignIn(WWW.EscapeURL(_email), WWW.EscapeURL(_password), OnComplete);
    }

    void OnComplete(object args) {

        if (ResponseIsValid(args))
            OnSignInSuccess(args);
        else
            OnSignInFail(args);
    }

    bool ResponseIsValid(object args) {
        /* rudimentary validation that can be improved by extracting all the argument fields */
       return args.ToString().Contains("status: \"valid\"");
    }

    void OnSignInSuccess(object token) {

        /* in a real world application the response token can be stored to disk then upon subsequent application startups we can validate this stored token against the server and skip the login page */

        _authServerLastSuccessResponse = token;
        SignInPage.SetActive(false);
        SignInSuccess.SetActive(true);
    }

    void OnSignInFail(object token) {

        /* The Unity JsonUtility threw an exception when serilising token to a simple object.
        This may be an oversight on my part but as we are only interested in the reason we can extract via regex match */

        var match = new Regex("(?<=reason:).*([\"\'])").Match(token.ToString());
        var reasonMessage = match.Success ? match.Value.Replace("\"", string.Empty) : "unknown error";

        FailReasonText.text = reasonMessage;
        SignInPage.SetActive(false);
        SignInFail.SetActive(true);
    }
}
