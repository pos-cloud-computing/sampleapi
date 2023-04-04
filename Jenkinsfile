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
                    sh "${scannerHome}/bin/sonar-scanner -Dsonar.projectKey=sampleapi -Dsonar.sources=. -Dsonar.host.url=http://52.54.105.156:9000 -Dsonar.login=sqp_69a1f58af7334eb28ee10f9704d835c46e77310"
                }
            }
        }
        stage ("Quality gate") {
            steps {
                waitForQualityGate abortPipeline: true
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

