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
        stage ('Sonarqube validation') {
            steps {
                script {
                    scannerHome = tool 'sonar'
                }
                withSonarQubeEnv('sq1'){
                    sh "${scannerHome}/bin/sonar-scanner -Dsonar.projectKey=sampleapi -Dsonar.sources=. -Dsonar.host.url=http://3.93.59.122:9000 -Dsonar.login=squ_70312d9e6984b0b573fcfdd1ce1ba5699a676db2"
                }
            }
        }
        stage('Test: Unit Test'){
        steps {
            sh 'dotnet test --logger "trx;LogFileName=UnitTests.trx"'
            }
        }
         stage ('Build Docker image') {
            steps {
                script {
                    dockerapp = docker.build("995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:${env.BUILD_ID}", '-f ./Web.API/Dockerfile . ')
                }
            }
        }
   }
}

