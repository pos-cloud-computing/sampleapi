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
        stage ('Build Docker image') {
            steps {
                script {
                    dockerapp = docker.build("995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:${env.BUILD_ID}", '-f ./Web.API/Dockerfile . ')
                }
            }
        }
        stage ('Sonarqube validation') {
            steps {
                script {
                    scannerHome = tool 'SonarScanner for MSBuild'
                }
                withSonarQubeEnv('SonarQube SKY Corp'){
                    sh "${scannerHome}/bin/sonar-scanner -Dsonar.projectKey=pos-cloud-computing -Dsonar.sources=. -Dsonar.host.url='http://54.175.167.78:9000/' -Dsonar.login=91489c5ef7ee79a055ac2c7c0f8cc4638e2e7fd6" 
                }
            }
        }
   }
}


