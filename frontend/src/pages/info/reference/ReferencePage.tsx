import { ReferenceHeader } from "@/widgets/info/reference/reference-header";
import styles from "./ReferencePage.module.css";
import Container from "@/shared/ui/layout/container";
import { ReferenceQuestions } from "@/widgets/info/reference/reference-questions";
import { ReferenceReturn } from "@/widgets/info/reference/reference-return";
import { ReferenceNeedHelp } from "@/widgets/info/reference/reference-need-help";

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
