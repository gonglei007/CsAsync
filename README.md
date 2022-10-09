# CsAsync
C#的异步流程控制库，用于异步流程方法调用管理的。让难读的异步嵌套代码变成易读的顺序流程代码。

## 说明文档
- Waterfall
- Whilst
- Each
----
### Waterfall类
顺序执行任务，当一个任务完成后调用callback进入下一个任务。可以让一系列序列执行的异步代码管理起来更清晰。

#### 使用案例：
有一系列网络接口请求，每一个请求得到服务器的回调后才能执行下一个请求，这样就可以用这个结构了。

#### 伪码例子：

```
        public void Task1(Exception e, Waterfall.CallbackDelegate callback)
        {
			DoRequest1(param, (string response)=>{
                callback(null);			// 调用callback才会进入下一个任务。
			});
        }

        public void Task2(Exception e, Waterfall.CallbackDelegate callback)
        {
			DoRequest2(param, (string response)=>{
                callback(null);		// 如果有异常传入Exception对象，这个顺序流将在此任务终止，直接到达结果。
			});
        }

		// 测试案例一
        public void DoTest1()
        {
            Waterfall waterfall = new Waterfall();
            waterfall.AddTask(Task1);
            waterfall.AddTask(Task2);
            waterfall.Start((Exception e) =>
            {
                Console.WriteLine("All done! -> " + e.ToString());		// 任务全部完成。
            });
        }

		// 测试案例二
        public void DoTest2()
        {
            Waterfall waterfall = new Waterfall();
            waterfall.AddTask((Exception e, Waterfall.CallbackDelegate callback)=>{
				DoRequest1(param, (string response)=>{
					callback(null);
				});
			});
            waterfall.AddTask((Exception e, Waterfall.CallbackDelegate callback)=>{
				DoRequest2(param, (string response)=>{
					callback(null);
				});
			});
            waterfall.Start((Exception e) =>
            {
                Console.WriteLine("All done! -> " + e.ToString());		// 任务全部完成。
            });
        }
```
----
### Whilst类
当测试条件成立，循环执行任务，当一个任务完成后调用callback进入下一次测试，如果测试失败，执行结果。

#### 伪码例子：

```
        public void DoTask(string strParam, Action<Exception> callback)
        {
            Console.WriteLine("Task-Begin");
            Timer timer = new Timer(1000);
            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
                (object source, ElapsedEventArgs eea) =>
                {
                    timer.Stop();
                    Console.WriteLine(strParam);
                    callback(null);
                }
            );
            Console.WriteLine("Task-End");
        }

        // Whilst测试案例
        public void DoTest()
        {
            string[] testString = { "item1", "item2", "item3"};
            int i = 0;
            Whilst whilst = new Whilst(
                // 测试
                ()=>{
                    return i < testString.Length;
                },
                // 执行
                (Action<Exception> callback) => {
                    string item = testString[i];
                    DoTask(item, (Exception e)=>{
                        i++;
                        if (item == "item2")
                        {
                            callback(new Exception("Exception test!" + item));
                        }
                        else {
                            callback(null);
                        }
                    });
                },
                // 结果
                (Exception e)=>{
                    if (e != null)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("All done!" );
            });
        }

```
----
### Each类
TODO：遍历数组。
----
## TODO
 - 实现taskcallback超时机制。任务设置：忽略（无响应，不继续）；重试（重试当前任务）；跳过（直接进入下一个任务）。
 - 任务Action支持传递数据参数。
 - 其它流程控制，如doWhilist、foreach...等。

## 参考
注：类似于nodejs的async.

## 交流讨论
技术交流QQ群：242500383
