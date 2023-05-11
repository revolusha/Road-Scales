public static class Encryptor
{
    public static string Encrypt(string value)
    {
        string encryptedValue = string.Empty;

        for (int i = 0; i < value.Length; i++)
            encryptedValue += (char)((int)value[i] + i);

        UnityEngine.Debug.Log("Encrypt " + encryptedValue);
        return encryptedValue;
    }

    public static string Decrypt(string encryptedValue)
    {
        string result = string.Empty;

        for (int i = 0; i < encryptedValue.Length; i++)
            result += (char)((int)encryptedValue[i] - i);

        UnityEngine.Debug.Log("Decrypt " + result);
        return result;
    }
}