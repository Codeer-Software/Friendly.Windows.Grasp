using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System.Diagnostics;
using System.Reflection;

namespace TestNetCore
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            WindowsAppFriend.SetCustomSerializer<CustomSerializer>();

            var targetPath = Path.GetFullPath(@"../../../../WinFormsApp/bin/Debug/net8.0-windows/WinFormsApp.exe");
            var info = new ProcessStartInfo(targetPath) { WorkingDirectory = Path.GetDirectoryName(targetPath) };
            var app = new WindowsAppFriend(Process.Start(info));

            var windowClass = string.Empty;
            {
                var form = app.WaitForIdentifyFromTypeFullName("WinFormsApp.MainForm");
                windowClass = form.WindowClassName;
                form.Activate();
            }
            {
                var form = app.WaitForIdentifyFromWindowClass(windowClass);
                form.Activate();
            }
            {
                var form = app.WaitForIdentifyFromWindowText("MainForm");
                form.Activate();
                form.GetChildren().ToList();
                form.GetDescendants().ToList();
                form.GetFromWindowText("button");
                form.GetFromBounds(1, 2, 3, 4);
                form.GetFromWindowClass("Button");
                form.GetWindowText();
                form.Close(new Codeer.Friendly.Async());
            }
        }

        public class IntPtrFormatter : IMessagePackFormatter<IntPtr>
        {
            public void Serialize(ref MessagePackWriter writer, IntPtr value, MessagePackSerializerOptions options)
                => writer.Write(value.ToInt64());

            public IntPtr Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
                => new IntPtr(reader.ReadInt64());
        }

        public class CustomSerializer : ICustomSerializer
        {
            MessagePackSerializerOptions customOptions = MessagePackSerializerOptions
                .Standard
                .WithResolver(
                    CompositeResolver.Create(
                        new IMessagePackFormatter[] { new IntPtrFormatter() },
                        new IFormatterResolver[] { TypelessContractlessStandardResolver.Instance, DynamicObjectResolverAllowPrivate.Instance, DynamicContractlessObjectResolverAllowPrivate.Instance }
                    )
                );

            public object Deserialize(byte[] bin)
                => MessagePackSerializer.Typeless.Deserialize(bin, customOptions);

            public Assembly[] GetRequiredAssemblies() => [GetType().Assembly, typeof(MessagePackSerializer).Assembly];

            public byte[] Serialize(object obj)
                => MessagePackSerializer.Typeless.Serialize(obj, customOptions);
        }
    }
}