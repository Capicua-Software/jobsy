using DATA_L.AuthenticationD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY_L.Models.Authentication;
using Firebase.Auth;
using Google.Cloud.Firestore;

namespace DATA_L.Authentication
{
    public class AuthenticationD:FirebaseCore
    {
        static string Default = "https://firebasestorage.googleapis.com/v0/b/jobsy-e4cf0.appspot.com/o/Jobs%2Fjobdefault.png?alt=media&token=95eb6412-f9df-4ce1-ad2b-9ed878923b8a";


        public async Task<LoginModel> LoginAsync(LoginModel model)
        {
            model = await GetUserInfo(model);
            model.auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            model.ab = await model.auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
            model.token = model.ab.FirebaseToken;
            model.user = model.ab.User;
            ENTITY_L.Models.Employer.EmployerModel.image = model.Logo;
            ENTITY_L.Models.Employer.EmployerModel.NameC = model.UserName;
            return model;
        }

        public async Task<SignUpModel> RegisterAsync(SignUpModel model)
        {
            model.auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            model.a = await model.auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
            await SendUserInfoToFirestore(model);
            return model;
        }

        //Returns user info based on Id
        public async Task<LoginModel> GetUserInfo(LoginModel model)
        {
            OpenFirestoreConnection(); // Establece la conexión
            try
            {
                //docRef = db.Collection("Users").Document(model.Email);

                DocumentSnapshot snapshot = await GetDocSnapshot(model.Email);

                if (snapshot.Exists)
                {
                    //model = snapshot.ConvertTo<LoginModel>();
                    Dictionary<string, object> user = snapshot.ToDictionary();
                    model.Role = (string)user["Role"];
                    model.UserName = (string)user["Name"];
                    model.Cedula = (string)user["Cedula"];
                    model.Logo = (string)user["Logo"];



                    return model;
                }
                else
                {
                    return new LoginModel();
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return model;
        }

        //This method returns an existing snapshot in the different roles that are in DB
        public async Task<DocumentSnapshot> GetDocSnapshot(string Id)
        {
            docRef = db.Collection("Admin").Document(Id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists) { return snapshot; }

            docRef = db.Collection("Users").Document(Id);
            snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists) { return snapshot; }

            docRef = db.Collection("Employers").Document(Id);
            snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists) { return snapshot; }

            return snapshot;

        } 

        public async Task SendUserInfoToFirestore(SignUpModel model)
        {
            OpenFirestoreConnection();
            model.Logo = Default;

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                {"Name", model.Name },
                {"Email", model.Email },
                {"Employer", model.Employer },
                {"Role", model.Role },
                {"Cedula", model.Cedula },
                {"Logo", model.Logo },
                {"valido", "false"}
            };

            switch (model.Employer)
            {
                case false:
                    user["Role"] = "User";
                    docRef = db.Collection("Users").Document(model.Email);
                    await docRef.SetAsync(user);
                    break;

                case true:
                    user["Role"] = "Employer";
                    docRef = db.Collection("Employers").Document(model.Email);
                    await docRef.SetAsync(user);
                    break;
            }

        }
    }
}
