/*
 * 从这个例子来看，无论将原本的对象强转成了其他的任意接口，或者是 object 之后，
 * 都能正确的获取到原本的类型。
 */

using System.Collections;
using System.Collections.Generic;

List<string> list = new List<string>();
IList<string> l1 = list;
IList l2 = (IList)l1;
object o = l2;
l1.GetType().Dump("IList<> Type");
l2.GetType().Dump("IList Type");
o.GetType().Dump("object type");