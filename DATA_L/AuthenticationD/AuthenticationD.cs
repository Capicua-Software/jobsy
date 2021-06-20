using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Authentication;
using Firebase.Auth;

namespace DATA_L.Authentication
{
    public class AuthenticationD:FirebaseCore
    {
        public async Task<LoginModel> LoginAsync(LoginModel model)
        {
            model.auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            model.ab = await model.auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
            model.token = model.ab.FirebaseToken;
            model.user = model.ab.User;
            return model;
        }

        public async Task<SignUpModel> RegisterAsync(SignUpModel model)
        {
            model.auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            model.a = await model.auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);

            return model;
        }
    }
}
