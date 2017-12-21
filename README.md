# CsAsync
C#的异步流程控制库，用于异步流程方法调用管理的。

## Waterfall类
顺序执行任务，当一个任务完成后调用callback进入下一个任务。可以让一系列序列执行的异步代码管理起来更清晰。

使用案例：有一系列网络接口请求，每一个请求得到服务器的回调后才能执行下一个请求，这样就可以用这个结构了。

伪码例子：
'''
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
            waterfall.AddTask((Exception e, , Waterfall.CallbackDelegate callback)=>{
				DoRequest1(param, (string response)=>{
					callback(null);
				});
			});
            waterfall.AddTask((Exception e, , Waterfall.CallbackDelegate callback)=>{
				DoRequest2(param, (string response)=>{
					callback(null);
				});
			});
            waterfall.Start((Exception e) =>
            {
                Console.WriteLine("All done! -> " + e.ToString());		// 任务全部完成。
            });
        }
'''

## 参考
注：类似于nodejs的async.
