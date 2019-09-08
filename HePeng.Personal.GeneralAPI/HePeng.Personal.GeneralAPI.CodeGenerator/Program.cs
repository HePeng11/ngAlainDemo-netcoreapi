using System;
using System.IO;

namespace HePeng.Personal.GeneralAPI.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var continuo = true;
            while (continuo)
            {
                continuo = product();
            }

        }


        private static bool product()
        {
            var modelNameTemple = "{{Scope_Name}}";
            var modelNameFirstLowTemple = "{{Scope_Name_firstLower}}";
            var modelDesiciptionTemple = "{{Scope_Description}}";
            var toptopPath = "../../../../";

            var a1 = "templates/ISysFileRepository.txt";
            var a = File.Exists(a1);

            var b1 = "templates/SysFileRepository.txt";
            var b = File.Exists(b1);

            var c1 = "templates/ISysFileService.txt";
            var c = File.Exists(c1);

            var d1 = "templates/SysFileService.txt";
            var d = File.Exists(d1);

            var k1 = "templates/TemplateController.txt";
            var k = File.Exists(k1);

            var iBaseRepository = toptopPath + "HePeng.Personal.Repository.Interface/IBaseRepository.cs";
            var e = File.Exists(iBaseRepository);
            var baseRepository = toptopPath + "HePeng.Personal.Repository.Implement/BaseRepository.cs";
            var f = File.Exists(baseRepository);
            var iBaseService = toptopPath + "HePeng.Personal.Service.Interface/IBaseService.cs";
            var g = File.Exists(iBaseService);
            var baseService = toptopPath + "HePeng.Personal.Service.Implement/BaseService.cs";
            var h = File.Exists(baseService);
            var controller = toptopPath + "HePeng.Personal.GeneralAPI/Controllers/BaseController.cs";
            var j = File.Exists(baseService);
            if (!(a && b && c && d && e && f && g && h && k && j))
            {
                Console.WriteLine("未找到文件模板或指定路径!");
            }

            Console.WriteLine("请输入EntityName,比如:SysUser,Order");
            var scope_name = Console.ReadLine();
            var scope_name_lower = scope_name.Substring(0, 1).ToLower() + scope_name.Substring(1, scope_name.Length - 1);
            Console.WriteLine("请输入Entity描述,比如：系统用户，订单");
            var scope_description = Console.ReadLine();

            var iXxxRepositoryContent = File.ReadAllText(a1);
            var path = new FileInfo(iBaseRepository).DirectoryName + $"\\I{scope_name}Repository.cs";
            File.AppendAllText(path, iXxxRepositoryContent.Replace(modelNameTemple, scope_name).Replace(modelDesiciptionTemple, scope_description).Replace(modelNameFirstLowTemple, scope_name_lower));
            Console.WriteLine($"生成成功:{path}");

            var XxxRepositoryContent = File.ReadAllText(b1);
            path = new FileInfo(baseRepository).DirectoryName + $"\\{scope_name}Repository.cs";
            File.AppendAllText(path, XxxRepositoryContent.Replace(modelNameTemple, scope_name).Replace(modelDesiciptionTemple, scope_description).Replace(modelNameFirstLowTemple, scope_name_lower));
            Console.WriteLine($"生成成功:{path}");

            var iXxxServiceContent = File.ReadAllText(c1);
            path = new FileInfo(iBaseService).DirectoryName + $"\\I{scope_name}Service.cs";
            File.AppendAllText(path, iXxxServiceContent.Replace(modelNameTemple, scope_name).Replace(modelDesiciptionTemple, scope_description).Replace(modelNameFirstLowTemple, scope_name_lower));
            Console.WriteLine($"生成成功:{path}");

            var xxxServiceContent = File.ReadAllText(d1);
            path = new FileInfo(baseService).DirectoryName + $"\\{scope_name}Service.cs";
            File.AppendAllText(path, xxxServiceContent.Replace(modelNameTemple, scope_name).Replace(modelDesiciptionTemple, scope_description).Replace(modelNameFirstLowTemple, scope_name_lower));
            Console.WriteLine($"生成成功:{path}");

            Console.WriteLine("是否生成控制器(y/n)?");
            var l = Console.ReadLine();
            if (l.ToLower() == "y")
            {
                var xxxController = File.ReadAllText(k1);
                path = new FileInfo(controller).DirectoryName + $"\\{scope_name}Controller.cs";
                File.AppendAllText(path, xxxController.Replace(modelNameTemple, scope_name).Replace(modelDesiciptionTemple, scope_description).Replace(modelNameFirstLowTemple, scope_name_lower));
                Console.WriteLine($"生成成功:{path}");
            }

            Console.WriteLine("输入exit退出,输入其它则继续生成");
            l = Console.ReadLine();
            if (l == "exit")
            {
                return false;
            }
            return true;
        }
    }
}
