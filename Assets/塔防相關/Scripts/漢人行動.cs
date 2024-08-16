using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class �~�H��� : MonoBehaviour
{
    NavMeshAgent �ɯ�;
    Animator �ʵe;

    public Transform �����ؼ�;
    GameObject[] �X�֥ؼ�;

    public int hp = 30;

    public float �g�{�Z�� = 2f;
    float �̵u�Z�� = 50f;

    public float �}�����Z = 1.6f;
    float fireTime;

    Text ��q;
    GameObject ���;
    int OriHP;

    public Rig rig;

    void Start()
    {
        ��q = transform.Find("Canvas/��q").gameObject.GetComponent<Text>();
        ��q.text = hp.ToString();
        ��� = transform.Find("Canvas/���").gameObject;
        OriHP = hp;
        rig.weight = 1;

        fireTime = �}�����Z;
        �ʵe = GetComponent<Animator>();
        �ɯ� = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameObject.Find("/target") == null)
        {
            ����ɯ�ü���ӧQ�ʵe();
            return;
        }

        �j�ؼ�();

        if (�����ؼ� == null)
        {
            ���m�̵u�Z��();
            return;
        }

        if (�����ؼ� != null && (�����ؼ�.tag == "���~�I" || �����ؼ�.name == "target"))
        {
            �p�G�ؼоa��h�P��();
        }
        else if (�����ؼ� != null && �����ؼ�.tag != "���~�I" && �����ؼ�.name != "target")
        {
            �p�G�b�g�{���h����();
        }
    }

    void ����ɯ�ü���ӧQ�ʵe()
    {
        �ɯ�.SetDestination(this.transform.position);
        �ɯ�.isStopped = true;
        �ʵe.SetTrigger("WIN");
    }

    void ���m�̵u�Z��()
    {
        �̵u�Z�� = 50f;
    }

    void �p�G�ؼоa��h�P��()
    {
        if (Vector3.Distance(�����ؼ�.position, this.transform.position) < 0.3f)
        {
            Destroy(�����ؼ�.gameObject);
        }
    }

    void �p�G�b�g�{���h����()
    {
        if (Vector3.Distance(this.transform.position, �����ؼ�.transform.position) < �g�{�Z��)
        {
            �ʵe.SetBool("Run", false);
            �ɯ�.isStopped = true;
            ���V�ؼ�();
            ���ն}��();
        }
    }

    void ���V�ؼ�()
    {
        this.transform.LookAt(�����ؼ�);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }

    void ���ն}��()
    {
        if (Time.time > fireTime)
        {
            �ʵe.SetTrigger("FIRE");
            fireTime = Time.time + �}�����Z;
        }
    }

    void �j�ؼ�()
    {
        GameObject[] ���~�I = GameObject.FindGameObjectsWithTag("���~�I");
        GameObject[] ������ = GameObject.FindGameObjectsWithTag("������");

        �X�֥ؼ� = ���~�I.Concat(������).ToArray();

        if (�X�֥ؼ�.Length == 0)
        {
            �]�m�����ؼЬ�Target();
        }
        else
        {
            ��̪ܳ񪺥ؼ�();
        }

        �p�G�������ؼЫh�ɯ�();
    }

    void �]�m�����ؼЬ�Target()
    {
        if (GameObject.Find("/target") != null)
        {
            �����ؼ� = GameObject.Find("/target").transform;
        }
    }

    void ��̪ܳ񪺥ؼ�()
    {
        float �ثe�Z��;
        foreach (GameObject �ؼ� in �X�֥ؼ�)
        {
            �ثe�Z�� = Vector3.Distance(�ؼ�.transform.position, this.transform.position);
            if (�ثe�Z�� < �̵u�Z��)
            {
                �̵u�Z�� = �ثe�Z��;
                �����ؼ� = �ؼ�.transform;
            }
        }
    }

    void �p�G�������ؼЫh�ɯ�()
    {
        if (�����ؼ� != null)
        {
            �ɯ�.isStopped = false;
            �ɯ�.SetDestination(�����ؼ�.position);
            �ʵe.SetBool("Run", true);
            �p�G�ؼФ��OTarget�h�ǻ��ؼе�����();
        }
    }

    void �p�G�ؼФ��OTarget�h�ǻ��ؼе�����()
    {
        if (�����ؼ�.name != "target")
        {
            �ǥؼе�����();
        }
    }

    void �ǥؼе�����()
    {
        if (this.transform.name == "�~�H_�A��_�S�Y(Clone)")
        {
            GetComponent<�~�H��A>().�ؼ� = �����ؼ�;
        }
        else if (this.transform.name == "�~�H-�}�b�� F1(Clone)")
        {
            GetComponent<�~�H�g�b>().�ؼ� = �����ؼ�;
        }

        transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
            = new WeightedTransformArray { new WeightedTransform(�����ؼ�, 1) };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "�������Z��")
        {
            �������(other);
        }
    }

    void �������(Collider other)
    {
        Destroy(other.gameObject);
        hp--;
        ��s��q���();
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void ��s��q���()
    {
        ��q.text = hp.ToString();
        float blood = (float)hp / (float)OriHP;
        ���.transform.localScale = new Vector3(blood, 1, 1);
    }
}
