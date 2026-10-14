import { ReferenceHeader } from "@/widgets/reference/reference-header";
import styles from "./ReferencePage.module.css";
import Container from "@/shared/ui/container";
import { ReferenceQuestions } from "@/widgets/reference/reference-questions";
import { ReferenceReturn } from "@/widgets/reference/reference-return";
import { ReferenceNeedHelp } from "@/widgets/reference/reference-need-help";

export default function ReferencePage() {
  return (
    <section className={styles.referencePage}>
      <Container>
        <div className={styles.referenceContent}>
          <ReferenceHeader />
          <div className={styles.referenceBody}>
            <div className={styles.referenceMain}>
              <ReferenceQuestions />
              <ReferenceReturn />
            </div>
            <ReferenceNeedHelp />
          </div>
        </div>
      </Container>
    </section>
  );
}
