using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace TinyMemFS
{
    [Serializable]
    class TinyMemFS
    {
        private HashSet<String> AllfileNames;
        /* key is fileName, 
         * value - list of [0]size & creation Date[1]
         */
        private List<KeyValuePair<String, List<String>>> MetaData;
        /*thread safe container for the metadata of file
          Key - file name
          Value - content of file in byte[]
         */
        private List<KeyValuePair<String, byte[]>> FSfiles;

        /*  probablyn need to add another dictionary wich pairs the fileName with a counter of how many times the file was encrypted
         *  so we could solve a situation when all our files are encrypted and we add a new file to the system.*/
        private List<KeyValuePair<String, List<int>>> nOfEncryptions_hidden_nCompress;//list[0] - numOfEncryptions, list[1] - hidden, list[2] - compressed
        [NonSerialized()] private Mutex mutex;
        private int counterOfEncryptions;
        public TinyMemFS()
        {
            // constructor
            AllfileNames = new HashSet<String>();
            MetaData = new List<KeyValuePair<String, List<String>>>();
            FSfiles = new List<KeyValuePair<String, byte[]>>();
            nOfEncryptions_hidden_nCompress = new List<KeyValuePair<String, List<int>>>();
            mutex = new Mutex();
            counterOfEncryptions = 0;

        }
        public bool add(String fileName, String fileToAdd)
        {
            // fileName - The name of the file to be added to the file system
            // fileToAdd - The file path on the computer that we add to the system
            // return false if operation failed for any reason
            // Example:
            // add("name1.pdf", "C:\\Users\\user\Desktop\\report.pdf")
            // note that fileToAdd isn't the same as the fileName
            List<String> SizeAndDate = new List<string>();
            List<int> EncHidComp = new List<int>(3);
            for (int i = 0; i < 3; i++)
                EncHidComp.Add(0);
            //define the KeyValuePairs
            KeyValuePair<String, List<String>> fileNameSizeAndDate;
            KeyValuePair<String, byte[]> fileNameContent;
            KeyValuePair<String, List<int>> fileNameEncHidComp;
            try
            {
                mutex.WaitOne();
                if (AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS already contains file: {0}", fileName));
                if (!File.Exists(fileToAdd))
                    throw new Exception(String.Format("Couldn't find {0}", fileToAdd));

                //read the file from the path and get it's info + content.
                FileInfo fileInfo = new FileInfo(fileToAdd);
                byte[] content = File.ReadAllBytes(fileInfo.FullName);
                SizeAndDate.Add(fileInfo.Length.ToString());
                SizeAndDate.Add(fileInfo.CreationTime.ToString());
                //creating the pairs
                fileNameSizeAndDate = new KeyValuePair<String, List<String>>(fileName, SizeAndDate);
                fileNameContent = new KeyValuePair<String, byte[]>(fileName, content);
                fileNameEncHidComp = new KeyValuePair<string, List<int>>(fileName, EncHidComp);
                MetaData.Add(fileNameSizeAndDate);
                FSfiles.Add(fileNameContent);
                nOfEncryptions_hidden_nCompress.Add(fileNameEncHidComp);
                AllfileNames.Add(fileName);// add fileName to set
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;
        }
        public bool remove(String fileName)
        {
            // fileName - remove fileName from the system
            // this operation releases all allocated memory for this file
            // return false if operation failed for any reason
            // Example:
            // remove("name1.pdf")
            try
            {
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS doesn't contain file: {0}", fileName));

                AllfileNames.Remove(fileName);
                for (int i = 0; i < MetaData.Count; i++)
                {
                    if (!MetaData[i].Key.Equals(fileName))
                        continue;
                    MetaData.RemoveAt(i);
                    FSfiles.RemoveAt(i);
                    nOfEncryptions_hidden_nCompress.RemoveAt(i);
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;
        }
        public List<String> listFiles()
        {
            // The function returns a list of strings with the file information in the system
            // Each string holds details of one file as following: "fileName,size,creation time"
            // Example:{
            // "report.pdf,630KB,Friday, ‎May ‎13, ‎2022, ‏‎12:16:32 PM",
            // "table1.csv,220KB,Monday, ‎February ‎14, ‎2022, ‏‎8:38:24 PM" }
            // You can use any format for the creation time and date
            String file = "";
            List<String> files = new List<string>();
            mutex.WaitOne();
            for (int i = 0; i < MetaData.Count; i++)
            {
                if (nOfEncryptions_hidden_nCompress[i].Value[1] == 1)//file is hidden
                    continue;
                long size = Int64.Parse(MetaData[i].Value[0]);
                String date = MetaData[i].Value[1];
                String fileName = MetaData[i].Key;
                file = String.Format("{0}, {1}, {2}", fileName, SizeSuffix(size), date);
                files.Add(file);
                file = "";
            }
            mutex.ReleaseMutex();
            return files;
        }

        public bool save(String fileName, String fileToAdd)
        {
            // this function saves file from the TinyMemFS file system into a file in the physical disk
            // fileName - file name from TinyMemFS to save in the computer
            // fileToAdd - The file path to be saved on the computer
            // return false if operation failed for any reason
            // Example:
            // save("name1.pdf", "C:\\tmp\\fileName.pdf")
            /*
             need to see an option about hiding the file if it's attribue is hidden
             */

            try
            {
                
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS doesn't contain file: {0}", fileName));
                if (File.Exists(fileToAdd))
                    throw new Exception(String.Format("file {0} already exists in disk", fileToAdd));
                FileInfo fileInfo = new FileInfo(fileToAdd);
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName))
                    {
                        File.WriteAllBytes(fileInfo.FullName, FSfiles[i].Value);
                        break;
                    }
                }
                mutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }

            return true;
        }
        public bool encrypt(String key)
        {
            // key - Encryption key to encrypt the contents of all files in the system 
            // You can use an encryption algorithm of your choice
            // return false if operation failed for any reason
            // Example:
            // encrypt("myFSpassword")
            try
            {

                mutex.WaitOne();
                if (key.Length > 32 || key.Length <= 0)
                    throw new Exception("The key must be with 32 characters at most!");
                if(FSfiles.Count==0)
                    throw new Exception("No Files in the TinyMemFS were detected!");
                counterOfEncryptions++;
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    String fileName = FSfiles[i].Key;
                    byte[] content = FSfiles[i].Value;//get content
                    byte[] encrypted = EncryptData(key, content);//encrypt with key
                    List<int> encryptionshiddencompress = nOfEncryptions_hidden_nCompress[i].Value;
                    encryptionshiddencompress[0] += 1;
                    FSfiles[i] = new KeyValuePair<string, byte[]>(fileName, encrypted);
                    nOfEncryptions_hidden_nCompress[i] = new KeyValuePair<string, List<int>>(fileName, encryptionshiddencompress);
                }

                mutex.ReleaseMutex();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            return true;
        }

        public bool decrypt(String key)
        {
            // fileName - Decryption key to decrypt the contents of all files in the system 
            // return false if operation failed for any reason
            // Example:
            // decrypt("myFSpassword")
            // check if it's encrypted if not raise error
            try
            {
                int counterOfDecryptions = 0;
                bool badKey = false;
                mutex.WaitOne();
                if (key.Length > 32 || key.Length <= 0)
                    throw new Exception("The key must be with 32 characters at most!");
                if (FSfiles.Count == 0)
                    throw new Exception("No Files in the TinyMemFS were detected!");
                if (counterOfEncryptions > 0)
                    counterOfEncryptions--;

                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (nOfEncryptions_hidden_nCompress[i].Value[0] == 0)//file wasn't encrypted
                        continue;
                    String fileName = FSfiles[i].Key;
                    byte[] encryptedContent = FSfiles[i].Value;//get content
                    try
                    {
                        byte[] decryptedContent = DecryptData(key, encryptedContent);//decrypt with key
                        counterOfDecryptions++;
                        //new list which holds the previous elements of encryption,decryption.
                        List<int> encryptionshiddencompress = nOfEncryptions_hidden_nCompress[i].Value;
                        encryptionshiddencompress[0] -= 1;
                        FSfiles[i] = new KeyValuePair<string, byte[]>(fileName, decryptedContent);
                        nOfEncryptions_hidden_nCompress[i] = new KeyValuePair<string, List<int>>(fileName, encryptionshiddencompress);
                    }
                    catch (Exception e)
                    {
                        //couldn't decrypt the data for some reasen
                        //Console.WriteLine(String.Format("File: {0} wasn't decrypted", fileName));
                        if(e.Message.Contains("Padding"))
                            MessageBox.Show(String.Format("Wrong Key, File: \"{0}\" wasn't decrypted", fileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        badKey = true;
                    }
                }
                if (badKey)// one of the files wasn't decrypted
                    throw new Exception(String.Format("Only {0} files were decrypted out of {1}", counterOfDecryptions, FSfiles.Count));
                mutex.ReleaseMutex();

            }
            catch (Exception ex)
            {
                /*if (ex.Message.Contains("Padding"))
                    Console.WriteLine("Wrong key given, decryption failed!");*/
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            return true;
        }
        private string SizeSuffix(long value)
        {
            int index = 1;
            string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB" };

            if (value == 0) { return string.Format("0 bytes"); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, index) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0}{1}",
                Decimal.ToInt64(adjustedSize),
                SizeSuffixes[mag]);
        }
        private byte[] EncryptData(string key, byte[] content)
        {
            byte[] iv = new byte[16];
            string fileString = Convert.ToBase64String(content);
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] tmp = new byte[32];
            using (Aes aes = Aes.Create())
            {
                for (int i = 0; i < keyByte.Length; i++)
                {
                    tmp[i] = keyByte[i];
                }
                aes.Key = tmp;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(fileString);
                        }
                        return memoryStream.ToArray();
                    }
                }
            }


        }

        private byte[] DecryptData(string key, byte[] encryptedArray)
        {
            byte[] iv = new byte[16];
            String text = Convert.ToBase64String(encryptedArray);
            byte[] buffer = Convert.FromBase64String(text);
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] tmp = new byte[32];
            string s;
            using (Aes aes = Aes.Create())
            {
                for (int i = 0; i < keyByte.Length; i++)
                {
                    tmp[i] = keyByte[i];
                }
                aes.Key = tmp;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            s = streamReader.ReadToEnd();
                        }
                        return Convert.FromBase64String(s);

                    }
                }
            }
        }
        // ************** NOT MANDATORY ********************************************
        // ********** Extended features of TinyMemFS ********************************
        public bool saveToDisk(String fileName)
        {
            /*
             * Save the FS to a single file in disk
             * return false if operation failed for any reason
             * You should store the entire FS (metadata and files) from memory to a single file.
             * You can decide how to save the FS in a single file (format, etc.) 
             * Example:
             * SaveToDisk("MYTINYFS.DAT")
             */
            try
            {
                mutex.WaitOne();
                if (!fileName.EndsWith(".TFS"))
                    throw new Exception("The format must be \".TFS\"");
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;


        }


        public bool loadFromDisk(String fileName)
        {
            /*
             * Load a saved FS from a file  
             * return false if operation failed for any reason
             * You should clear all the files in the current TinyMemFS if exist, before loading the filenName
             * Example:
             * LoadFromDisk("MYTINYFS.DAT")
             */
            try
            {
                mutex.WaitOne();
                if (!File.Exists(fileName))
                    throw new Exception(String.Format("Couldn't find {0}", fileName));
                if (!fileName.EndsWith(".TFS"))
                    throw new Exception("The format must be \".TFS\"");

                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    TinyMemFS loadedSystem = (TinyMemFS)binaryFormatter.Deserialize(stream);
                    this.MetaData = loadedSystem.MetaData;
                    this.FSfiles = loadedSystem.FSfiles;
                    this.AllfileNames = loadedSystem.AllfileNames;
                    this.nOfEncryptions_hidden_nCompress = loadedSystem.nOfEncryptions_hidden_nCompress;
                    this.counterOfEncryptions = loadedSystem.counterOfEncryptions;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;
        }

        public bool compressFile(String fileName)
        {
            /* Compress file fileName
             * return false if operation failed for any reason
             * You can use an compression/uncompression algorithm of your choice
             * Note that the file size might be changed due to this operation, update it accordingly
             * Example:
             * compressFile ("name1.pdf");
             */
            try
            {
                int position = 0;
                long size;
                byte[] data;
                String sSize;
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS doesn't contain file: {0}", fileName));
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName))
                    {
                        data = FSfiles[i].Value;// get the data
                        MemoryStream output = new MemoryStream();
                        using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
                        {
                            dstream.Write(data, 0, data.Length);//compress
                        }
                        data = output.ToArray();//writing back the compressed data to array
                        FSfiles[i] = new KeyValuePair<string, byte[]>(fileName, data);//update the list 
                        List<String> OldMetaDataInfo = MetaData[i].Value;// get the old metaData
                        OldMetaDataInfo[0] = data.Length.ToString();//add the new size of the compressed data
                        List<int> EncHidZipList = nOfEncryptions_hidden_nCompress[i].Value;//increase the number of compressed items
                        EncHidZipList[2] += 1;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;

        }

        public bool uncompressFile(String fileName)
        {
            /* uncompress file fileName
             * return false if operation failed for any reason
             * You can use an compression/uncompression algorithm of your choice
             * Note that the file size might be changed due to this operation, update it accordingly
             * Example:
             * uncompressFile ("name1.pdf");
             */
            try
            {
                int position = 0;
                long size;
                byte[] data;
                String sSize;
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS doesn't contain file: {0}", fileName));

                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName))
                    {
                        data = FSfiles[i].Value;// get the data
                        MemoryStream input = new MemoryStream(data);
                        MemoryStream output = new MemoryStream();
                        using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
                        {
                            dstream.CopyTo(output);
                        }
                        data = output.ToArray();//writing back the compressed data to array
                        FSfiles[i] = new KeyValuePair<string, byte[]>(fileName, data);//update the list 
                        List<String> OldMetaDataInfo = MetaData[i].Value;// get the old metaData
                        OldMetaDataInfo[0] = data.Length.ToString();//add the new size of the compressed data
                        List<int> EncHidZipList = nOfEncryptions_hidden_nCompress[i].Value;//increase the number of compressed items
                        EncHidZipList[2] -= 1;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }

            mutex.ReleaseMutex();
            return true;

        }


        public bool setHidden(String fileName, bool hidden)
        {
            /* set the hidden property of fileName
             * If file is hidden, it will not appear in the listFiles() results
             * return false if operation failed for any reason
             * Example:
             * setHidden ("name1.pdf", true);
             */
            try
            {
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("The TinyMemFS doesn't contain file: \"{0}", fileName));

                for (int i = 0; i < FSfiles.Count; i++)//search the system for the file
                {
                    if (FSfiles[i].Key.Equals(fileName))//found the file in the system
                    {
                        List<int> encryptionshiddencompress = new List<int>(nOfEncryptions_hidden_nCompress[i].Value);
                        encryptionshiddencompress[1] = hidden == true ? 1 : 0;//set the hidden value 
                        nOfEncryptions_hidden_nCompress[i] = new KeyValuePair<string, List<int>>(fileName, encryptionshiddencompress);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            mutex.ReleaseMutex();
            return true;
        }

        public bool rename(String fileName, String newFileName)
        {
            /* Rename filename to newFileName
             * Return false if operation failed for any reason (E.g., newFileName already exists)
             * Example:
             * rename ("name1.pdf", "name2.pdf");
             */
            try
            {
                mutex.WaitOne();
                if (AllfileNames.Contains(newFileName))
                    throw new Exception(String.Format("TinyMemFS already contains \"{0}\"", newFileName));
                if (!AllfileNames.Contains(fileName))
                    throw new Exception(String.Format("TinyMemFS doesn't contain \"{0}\"", fileName));
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName))
                    {
                        AllfileNames.Remove(fileName);
                        AllfileNames.Add(newFileName);
                        RenameFileInTinyMemFSHelper(newFileName, i);
                        break;
                    }
                }
                mutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                mutex.ReleaseMutex();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void RenameFileInTinyMemFSHelper(string newFileName, int position)
        {
            //getting the values from each list
            List<String> SizeAndDate = new List<String>(MetaData[position].Value);//get size and date
            List<int> EncHidComp = new List<int>(nOfEncryptions_hidden_nCompress[position].Value);//get encryption and etc
            byte[] content = FSfiles[position].Value;//get data
                                                     //define the KeyValuePairs with the new file name
            KeyValuePair<String, List<String>> fileNameSizeAndDate =
                new KeyValuePair<String, List<String>>(newFileName, SizeAndDate);
            KeyValuePair<String, byte[]> fileNameContent = new KeyValuePair<String, byte[]>(newFileName, content);
            KeyValuePair<String, List<int>> fileNameEncHidComp = new KeyValuePair<string, List<int>>(newFileName, EncHidComp);
            //remove the old
            MetaData.RemoveAt(position);
            FSfiles.RemoveAt(position);
            nOfEncryptions_hidden_nCompress.RemoveAt(position);
            //add to the data structures
            MetaData.Insert(position, fileNameSizeAndDate);
            FSfiles.Insert(position, fileNameContent);
            nOfEncryptions_hidden_nCompress.Insert(position, fileNameEncHidComp);
        }

        public bool copy(String fileName1, String fileName2)
        {
            /* Copy the content,size and creation date of one file to another.
             * The file name will not change
             * Return false if operation failed for any reason (E.g., fileName1 doesn't exist or filename2 already exists)
             * Example:
             * copy("name1.pdf", "name2.pdf");
             */
            //need the content and the size
            /*we will copy the content of fileName1 to fileName2*/
            try
            {
                int file1Pos = -1, file2Pos = -1;
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName1))
                    throw new Exception(String.Format("TinyMemFS doesn't contains \"{0}\"", fileName1));
                if (!AllfileNames.Contains(fileName2))
                    throw new Exception(String.Format("TinyMemFS doesn't contain \"{0}\"", fileName2));
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName1))
                        file1Pos = i;
                    if (FSfiles[i].Key.Equals(fileName2))
                        file2Pos = i;
                    if (file1Pos != -1 && file2Pos != -1)//found both positions
                        break;
                }
                byte[] content = new byte[FSfiles[file1Pos].Value.LongLength];// create a new array
                FSfiles[file1Pos].Value.CopyTo(content, 0);//copy the content from file2 to file1
                List<String> SizeAndDate = new List<String>(MetaData[file1Pos].Value);//get the date and size of fileName2
                List<int> EncHidComp = new List<int>(nOfEncryptions_hidden_nCompress[file1Pos].Value);//get the encryption detail of file 2
                EncHidComp[1] = nOfEncryptions_hidden_nCompress[file2Pos].Value[1];//keep the hidden value of file1
                                                                                   //creating the pairs
                KeyValuePair<String, List<String>> fileNameSizeAndDate =
                new KeyValuePair<String, List<String>>(fileName2, SizeAndDate);
                KeyValuePair<String, byte[]> fileNameContent = new KeyValuePair<String, byte[]>(fileName2, content);
                KeyValuePair<String, List<int>> fileNameEncHidComp = new KeyValuePair<string, List<int>>(fileName2, EncHidComp);
                //remove the old content
                MetaData.RemoveAt(file2Pos);
                FSfiles.RemoveAt(file2Pos);
                nOfEncryptions_hidden_nCompress.RemoveAt(file2Pos);
                //add the new copied file to the data structures
                MetaData.Insert(file2Pos, fileNameSizeAndDate);
                FSfiles.Insert(file2Pos, fileNameContent);
                nOfEncryptions_hidden_nCompress.Insert(file2Pos, fileNameEncHidComp);
                mutex.ReleaseMutex();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.ReleaseMutex();
                return false;
            }
            return true;
        }

        private void sorterOfEncHidcomp()
        {
            List < KeyValuePair < String, List<int> >> helperArray = new List<KeyValuePair<string, List<int>>> ();
            for (int i=0; i<MetaData.Count; i++)
            {
                for(int j=0; j < nOfEncryptions_hidden_nCompress.Count; j++)
                {
                    if(MetaData[i].Key.Equals(nOfEncryptions_hidden_nCompress[j].Key))// we found the names
                    {
                        helperArray.Insert(i,nOfEncryptions_hidden_nCompress[j]);
                    }
                }
            }
            nOfEncryptions_hidden_nCompress = helperArray;
        }
        private void sorterOfFSFiles()
        {
            List<KeyValuePair<String, Byte[]>> helperArray = new List<KeyValuePair<string, Byte[]>>();
            for (int i = 0; i < MetaData.Count; i++)
            {
                for (int j = 0; j < FSfiles.Count; j++)
                {
                    if (MetaData[i].Key.Equals(FSfiles[j].Key))// we found the names
                    {
                        helperArray.Insert(i, FSfiles[j]);
                    }
                }
            }
            FSfiles = helperArray;
        }
        public void sortByName()
        {
            /* Sort the files in the FS by their names (alphabetical order)
             * This should affect the order the files appear in the listFiles 
             * if two names are equal you can sort them arbitrarily
             */
            mutex.WaitOne();
            TinyMemFSSorter sortByName = new TinyMemFSSorter(0);
            MetaData.Sort(sortByName);
            sorterOfEncHidcomp();
            sorterOfFSFiles();
            mutex.ReleaseMutex();
        }

        public void sortByDate()
        {
            /* Sort the files in the FS by their date (new to old)
             * This should affect the order the files appear in the listFiles  
             * if two dates are equal you can sort them arbitrarily
             */
            mutex.WaitOne();
            TinyMemFSSorter sortByDate = new TinyMemFSSorter(2);
            MetaData.Sort(sortByDate);
            sorterOfEncHidcomp();
            sorterOfFSFiles();
            mutex.ReleaseMutex();
        }

        public void sortBySize()
        {
            /* Sort the files in the FS by their sizes (large to small)
             * This should affect the order the files appear in the listFiles  
             * if two sizes are equal you can sort them arbitrarily
             */
            mutex.WaitOne();
            TinyMemFSSorter sortBySize = new TinyMemFSSorter(1);
            MetaData.Sort(sortBySize);
            sorterOfEncHidcomp();
            sorterOfFSFiles();
            mutex.ReleaseMutex();
        }


        public bool compare(String fileName1, String fileName2)
        {
            /* compare fileName1 and fileName2
             * files considered equal if their content is equal 
             * Return false if the two files are not equal, or if operation failed for any reason (E.g., fileName1 or fileName2 not exist)
             * Example:
             * compare ("name1.pdf", "name2.pdf");
             */
            byte[] content1 = null, content2 = null;
            try
            {
                mutex.WaitOne();
                if (!AllfileNames.Contains(fileName1))
                    throw new Exception(String.Format("File system doesn't contain file \"{0}\"", fileName1));
                if (!AllfileNames.Contains(fileName2))
                    throw new Exception(String.Format("File system doesn't contain file \"{0}\"", fileName2));
                for (int i = 0; i < FSfiles.Count; i++)
                {
                    if (FSfiles[i].Key.Equals(fileName1))
                        content1 = FSfiles[i].Value;
                    if (FSfiles[i].Key.Equals(fileName2))
                        content2 = FSfiles[i].Value;
                }
            }
            catch (Exception e)
            {
                mutex.ReleaseMutex();
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            mutex.ReleaseMutex();
            return Enumerable.SequenceEqual(content1, content2);
        }

        public Int64 getSize()
        {
            /* return the size of all files in the FS (sum of all sizes)
             */
            long totalSize = 0;
            mutex.WaitOne();
            for (int i = 0; i < MetaData.Count; i++)
                totalSize += Int64.Parse(MetaData[i].Value[0]);
            mutex.ReleaseMutex();
            return totalSize;
        }
        /* a comparer for the sorting the file in TineMemFS*/
        private class TinyMemFSSorter : IComparer<KeyValuePair<String, List<String>>>
        {
            // sort by name, size or date
            public int howToSort { get; set; }


            public TinyMemFSSorter(int sortingOption)
            {
                howToSort = sortingOption;
            }


            public int Compare(KeyValuePair<String, List<String>> x, KeyValuePair<String, List<String>> y)
            {
                int compareResult = 0;
                long size1, size2;//size comparison
                DateTime date1, date2;//date comparison
                KeyValuePair<String, List<String>> itemX = x;
                KeyValuePair<String, List<String>> itemY = y;
                // compare the 2 items depending on the selected column
                switch (howToSort)
                {
                    case 0://compareByName
                        compareResult = string.Compare(itemX.Key, itemY.Key);
                        break;
                    case 1://compareBySize
                        size1 = Int64.Parse(itemX.Value[0]);
                        size2 = Int64.Parse(itemY.Value[0]);
                        compareResult = size1.CompareTo(size2);
                        break;
                    case 2://CompareByDate
                        date1 = DateTime.Parse(itemX.Value[1]);
                        date2 = DateTime.Parse(itemY.Value[1]);
                        compareResult = DateTime.Compare(date1, date2);
                        break;
                }
                return compareResult;

            }
        }

    }

}