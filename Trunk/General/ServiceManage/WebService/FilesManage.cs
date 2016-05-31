using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.FilesManage;
using ServiceManage.FileMgtService;

namespace ServiceManage.WebService
{
    public class FilesManage : AbstracFilesManage
    {
        FileMgtSoapClient soap;
        public FilesManage()
        {
            try
            {
                soap = new FileMgtSoapClient();


            }
            catch
            {
                Exception Ex = new Exception("请正确配置文件管理的节点！");
                throw Ex;
            }

        }

        public override ReturnValueInfo SaveBytes(string project, string item, string fullName, byte[] bytes)
        {
            try
            {
                ReturnValueInfo returnInfo = soap.SaveBytes(project, item, fullName, bytes);
                return returnInfo;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override ReturnValueInfo UpdateBytes(Guid recordId, string project, string item, string fullName, byte[] bytes)
        {
            try
            {
                ReturnValueInfo returnInfo = soap.UpdateBytes(recordId, project, item, fullName, bytes);
                return returnInfo;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override ReturnValueInfo UpdateById(Guid recordId, string fileName, byte[] bytes)
        {
            try
            {
                ReturnValueInfo returnInfo = soap.UpdateById(recordId, fileName, bytes);
                return returnInfo;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override string GetFileRelativePath(Guid recordId)
        {
            try
            {

                return soap.GetFileRelativePath(recordId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override byte[] GetFileBytes(Guid recordId)
        {
            try
            {

                return soap.GetFileBytes(recordId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override Pro_File GetPro_File(Guid recordId)
        {
            try
            {

                return soap.GetPro_File(recordId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override List<Pro_File> GetPro_Files(List<Guid> recordIds)
        {
            try
            {


                return soap.GetPro_Files((ArrayOfGuid)recordIds).ToList();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override ReturnValueInfo DeletePro_File(Guid recordId)
        {
            try
            {
                return soap.DeletePro_File(recordId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public override List<string> GetFileRelativePaths(List<Guid> recordIds)
        {
            try
            {

                return soap.GetFileRelativePaths((ArrayOfGuid)recordIds).ToList();
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        public override ReturnValueInfo Save(string project, string item, string filePath)
        {
            try
            {

                return soap.Save(project, item, filePath);


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ReturnValueInfo SaveHttpFile(string project, string item, string uri)
        {
            try
            {
                ReturnValueInfo ReturnInfo = new ReturnValueInfo();

                ReturnInfo = soap.SaveHttpFile(project, item, uri);

                return ReturnInfo;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }
}
