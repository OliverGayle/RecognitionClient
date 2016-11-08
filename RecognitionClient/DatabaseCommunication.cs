using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV;

namespace RecognitionClient
{
    class DatabaseCommunication
    {
    }
    interface IDataStoreAccess
    {
        String SaveFace(String username, Byte[] faceBlob);

        List<Face> CallFaces(String username);

        bool IsUsernameValid(String username);

        String SaveAdmin(String username, String password);

        bool DeleteUser(String username);

        int GetUserId(String username);

        int GenerateUserId();

        String GetUsername(int userId);

        List<String> GetAllUsernames();
    }
    internal class Face
    {
        public byte[] Image { get; set; }

        public int Id { get; set; }

        public String Label { get; set; }

        public int UserId { get; set; }
    }
}
