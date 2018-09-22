using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ClassLib;

namespace TestApp
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Test test = new Test();
            int result = test.AddNumbers(42, 21);
            Console.WriteLine(result);
            test.Dispose();

        }
    }

    public class Test : IDisposable
    {
        #region PInvokes
        //Anropar extern "C" i ClassLib för att skapa en class och returnera pointern till denna class hit.
        [DllImport("ClassLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateTestClass();

        //Anropar extern "C" i classLib för att ta bort objektet, kräver en pointer till objektet.
        [DllImport("ClassLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisposeTestClass(IntPtr pTestClassObject);

        //Anropar extern "C", i ClassLib, funktionen Add som är en member av Ovan nämnda class.
        [DllImport("ClassLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(IntPtr pTestClassObject, ref int a, ref int b);
        #endregion

        #region Members
        private IntPtr pObjekt;
        #endregion

        public Test()
        {
            this.pObjekt = CreateTestClass();
        }

        ~Test()
        {
            //Deconstructor anropas om ifall inte Dipose() har körts och tar hand om allt. Typ en failsafe helt enkelt.
            Dispose(false);
        }

        public void Dispose()
        {
            //Anropar Dispose() och tar bort objektet och referensen.
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if(this.pObjekt != IntPtr.Zero)
            {
                //Ta bort objektet i DLL filen och referensen här.
                DisposeTestClass(this.pObjekt);
                this.pObjekt = IntPtr.Zero;
            }

            if (isDisposing)
            {
                //Om dispose anropas med true så behövs inte GC.
                GC.SuppressFinalize(this);
            }
        }

        #region WrapperMethods
        public int AddNumbers(int num1, int num2)
        {
            return Add(pObjekt, ref num1, ref num2);
        }
        #endregion

    }
}
