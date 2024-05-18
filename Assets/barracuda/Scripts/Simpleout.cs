using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
public class Simpleout : MonoBehaviour {
    private Model model;
    private IWorker worker;

    // Start is called before the first frame update
    void Start() {
        model = CreateModel();
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);
        var input = new Tensor(1, 28, 28, 1);
        worker.Execute(input);
        var output = worker.PeekOutput();
        Debug.Log(output);
        Debug.Log(worker.Summary());
        input.Dispose();
        output.Dispose();
        worker.Dispose();
    }
    public Model CreateModel() {
        var modelBuilder = new ModelBuilder(new Model());

        // 添加一个输入层
        var input = modelBuilder.Input("input", new TensorShape(1, 28, 28, 1));
        int[] pad = new int[] { 1, 1, 1, 1 };
        int[] stride = new int[] { 1, 1 };
        Tensor kernel = new Tensor(3, 3, 1, 1);
        Tensor bias = new Tensor(1, 1);
        // 添加一个卷积层
        var conv = modelBuilder.Conv2D("conv", input, stride, pad, kernel, bias);

        // 添加一个ReLU激活层
        var relu = modelBuilder.Relu("relu", conv);
        Tensor weight = new Tensor(784, 10);
        bias = new Tensor(1, 1);
        // 添加一个全连接层
        var dense = modelBuilder.Dense("dense", relu, weight, bias);

        // 添加一个Softmax激活层
        var softmax = modelBuilder.Softmax("softmax", dense);

        // 设置输出层
        modelBuilder.Output(softmax);

        // 获取模型
        return modelBuilder.model;
    }
    // Update is called once per frame

}


