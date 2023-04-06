pipeline {
    agent any


    stages {

        stage ('Clean workspace') {
            steps {
                cleanWs()
            }
        }
        stage ('Checkout') {
            steps {
                script {
                    checkout scm
                }
            }
        }
        stage('Test: Unit Test'){
        steps {
            sh 'dotnet test --logger "trx;LogFileName=UnitTests.trx"'
            }
        }
        stage('Publish Image ECR'){
            steps {
                withEnv(["AWS_ACCESS_KEY_ID=${env.AWS_ACCESS_KEY_ID}", "AWS_SECRET_ACCESS_KEY=${env.AWS_SECRET_ACCESS_KEY}", "AWS_DEFAULT_REGION=${env.AWS_DEFAULT_REGION}"]){
                 sh 'docker login -u AWS -p $(aws ecr-public get-login-password --region us-east-1) public.ecr.aws/p3q4d0z4'
                 sh 'docker build -t public.ecr.aws/p3q4d0z4/sampleapi:""$BUILD_ID"" -f ./Web.API/Dockerfile .' 
                 sh 'docker push public.ecr.aws/p3q4d0z4/sampleapi:""$BUILD_ID""'
                 sh 'docker push public.ecr.aws/p3q4d0z4/sampleapi:latest'
                }
            }
        }

        stage ('Deploy Ambiente de HML') {
            environment {
                tag_version = "${env.BUILD_ID}"
            }
            steps {
                withKubeConfig ([credentialsId: 'eks-hml']) {
                    sh 'sed -i "s/{{TAG}}/$tag_version/g" ./k8s/deployment.yaml'
                    sh 'kubectl apply -f ./k8s/deployment.yaml'
                }                
            }
        }
   }
}

