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
        stage('Publish ECR'){
            steps {
                withEnv(["AWS_ACCESS_KEY_ID=${env.AWS_ACCESS_KEY_ID}", "AWS_SECRET_ACCESS_KEY=${env.AWS_SECRET_ACCESS_KEY}", "AWS_DEFAULT_REGION=${env.AWS_DEFAULT_REGION}"]){
                 sh 'docker login -u AWS -p $(aws ecr get-login-password --region us-east-1) 995396735443.dkr.ecr.us-east-1.amazonaws.com'
                 sh 'docker build -t sampleapi -f ./Web.API/Dockerfile .' 
                 sh 'docker tag sampleapi:latest 995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:latest'
                 sh 'docker push 995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:latest'
                }
            }
        }
   }
}

