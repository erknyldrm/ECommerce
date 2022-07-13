using ECommerce.Infrastructure.Operations;

namespace ECommerce.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string fileName, string pathOrContainerName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);

                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;

                    int index1 = newFileName.IndexOf('-');
                    if (index1 == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = index1;
                            index1 = newFileName.IndexOf('-', index1 + 1);
                            if (index1 == -1)
                            {
                                index1 = lastIndex;
                                break;
                            }
                        }

                        int index2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(index1 + 1, index2 - index1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(index1 + 1, index2 - index1 - 1)
                                                     .Insert(index1 + 1, _fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }

               if(hasFileMethod(newFileName,pathOrContainerName))
                    return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
                else
                    return newFileName;

            });

            return newFileName;
        }
    }
}
