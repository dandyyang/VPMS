using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceManage.FileMgtService;

namespace ServiceManage.FilesManage
{
    public abstract class AbstracFilesManage
    {
        /// <summary>
        /// 保存文件到文件服务器        
        /// </summary>
        /// <param name="project">调用此服务的项目名称</param>
        /// <param name="item">调用此服务的项目模块名称</param>
        /// <param name="bytes">文件数据</param>
        /// <returns></returns>  
        public abstract ReturnValueInfo SaveBytes(string project, string item, string fullName, byte[] bytes);


        public abstract ReturnValueInfo Save(string project, string item, string filePath);


        /// </summary>
        /// <param name="project">调用此服务的项目名称</param>
        /// <param name="item">调用此服务的项目模块名称</param>
        /// <param name="bytes">文件数据</param>
        /// <returns></returns>     
        public abstract ReturnValueInfo UpdateBytes(Guid recordId, string project, string item, string fullName, byte[] bytes);

        /// <summary>
        /// 更新文件到文件服务器
        /// </summary>
        /// <param name="recordId">更新记录KEY</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="bytes">文件数据</param>
        /// <returns></returns>
        public abstract ReturnValueInfo UpdateById(Guid recordId, string fileName, byte[] bytes);


        public abstract string GetFileRelativePath(Guid recordId);

        public abstract byte[] GetFileBytes(Guid recordId);

        public abstract Pro_File GetPro_File(Guid recordId);

        public abstract List<Pro_File> GetPro_Files(List<Guid> recordIds);

        public abstract ReturnValueInfo DeletePro_File(Guid recordId);

        public abstract List<string> GetFileRelativePaths(List<Guid> recordIds);
    }
}
